namespace Shop.ApplicationServices.SpaceshipServices;

using Accuweather;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Json;
using Shop.Core.Dto.Weather;
using Shop.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class WeatherForecastService : IWeatherForecastService
{
    private const bool CACHE_MISS_ALLOWED = true;
    private const string apiKey = "qLR2bZhfkkCaTp40WtnQKCUAz0MuY5OG";
    private static readonly string CACHE_PATH = Path.GetFullPath("./.accuweather-cache.json");

    private ServiceState? _state = null;
    private bool _rateLimitReached = false;

    public WeatherForecastService()
    {
    }

    public async Task<LocationWeatherResultDto?> GetWeather(string cityName)
    {
        string url = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={cityName}";

        using (WebClient client = new WebClient())
        {
            string? json = await SendRequest(url, client);

            if (json == null)
            { 
                if (_state != null)
                {
                    _state.RateLimitRemaining = 0;
                    await SaveCache();
                }

                return new LocationWeatherResultDto
                {
                    CityName = cityName,
                    CurrentConditions = [],
                    RateLimitRemaining = 0,
                    RateLimit = 50
                };
            }

            JavaScriptSerializer js = new();

            List<WeatherLocationRootDto> list = js.Deserialize<List<WeatherLocationRootDto>>(json);
            List<CurrentConditionDto> results = [];

            foreach (var item in list)
            {
                var gotten = await GetCurrentConditions(js, client, item);
                if (gotten == null)
                {
                    continue;
                }

                results.AddRange(gotten);
            }

            LocationWeatherResultDto resultDto = new LocationWeatherResultDto();
            resultDto.CityName = cityName;
            resultDto.CurrentConditions = results;

            if (_state == null)
            {
                resultDto.RateLimit = 50;
                resultDto.RateLimitRemaining = 50;
            } 
            else
            {
                resultDto.RateLimit = _state.RateLimit;
                resultDto.RateLimitRemaining = _state.RateLimitRemaining;
            }

            return resultDto;
        }
    }

    private async Task<List<CurrentConditionDto>?> GetCurrentConditions(JavaScriptSerializer js, WebClient client, WeatherLocationRootDto dto)
    {
        string currentConditionUrl = $"http://dataservice.accuweather.com/currentconditions/v1/{dto.Key}?apikey={apiKey}";
        string? json = await SendRequest(currentConditionUrl, client);

        if (json == null)
        {
            return null;
        }

        List<CurrentConditionDto> list = js.Deserialize<List<CurrentConditionDto>>(json);

        foreach (var item in list)
        {
            item.Location = dto;
        }

        return list;
    }

    private async Task<string?> SendRequest(string url, WebClient client)
    {
        if (_state == null)
        {
            await LoadCache();
        }

        string? jsonContent;

        if (_state.ReponseCache.ContainsKey(url))
        {
            jsonContent = _state.ReponseCache[url];
        } 
        else
        {
            if (_rateLimitReached)
            {
                _state.RateLimitRemaining = 0;
                _state.RateLimit = 50;

                await SaveCache();

                return null;
            }

            WebHeaderCollection replyHeaders;

            try
            {
                jsonContent = client.DownloadString(url);
                replyHeaders = client.ResponseHeaders;
                _state.ReponseCache[url] = jsonContent;
            } 
            catch (WebException exc)
            {
                jsonContent = null;
                replyHeaders = exc.Response.Headers;
                _rateLimitReached = true;
            }

            _state.RateLimit = int.Parse(replyHeaders["RateLimit-Limit"] ?? "50");
            _state.RateLimitRemaining = int.Parse(replyHeaders["RateLimit-Remaining"] ?? "50");

            await SaveCache();
        }

        return jsonContent;
    }

    private async Task LoadCache()
    {
        if (!Path.Exists(CACHE_PATH))
        {
            _state = new();
            return;
        }

        string jsonContent;

        using (StreamReader reader = new(CACHE_PATH))
        {
            jsonContent = await reader.ReadToEndAsync();
        }

        var state = JsonSerializer.Deserialize<ServiceState>(jsonContent);
        _state = state;
    }

    private async Task SaveCache()
    {
        if (_state == null)
        {
            return;
        }

        var jsonString = JsonSerializer.Serialize(_state);

        using (StreamWriter writer = new(CACHE_PATH))
        {
            await writer.WriteAsync(jsonString);
        }
    }
}

internal class ServiceState
{
    public Dictionary<string, string> ReponseCache { get; set; } = [];
    public int RateLimit { get; set; } = 50;
    public int RateLimitRemaining { get; set; } = 50;
}
