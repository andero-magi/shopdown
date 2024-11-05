namespace Shop.ApplicationServices.SpaceshipServices;

using Accuweather;
using Nancy.Json;
using Shop.Core.Dto.Weather;
using Shop.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public class WeatherForecastService: IWeatherForecastService
{
    private const string apiKey = "qLR2bZhfkkCaTp40WtnQKCUAz0MuY5OG";

    public WeatherForecastService()
    {
    }

    public async Task<LocationWeatherResultDto> GetWeather(string cityName)
    {
        string url = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={cityName}";

        using (WebClient client = new WebClient()) 
        {
            string json = client.DownloadString(url);
            JavaScriptSerializer js = new();

            List<WeatherLocationRootDto> list = js.Deserialize<List<WeatherLocationRootDto>>(json);
            List<CurrentConditionDto> results = [];

            foreach (var item in list)
            {
                var gotten = await GetCurrentConditions(js, client, item);
                results.AddRange(gotten);
            }

            LocationWeatherResultDto resultDto = new LocationWeatherResultDto();
            resultDto.CityName = cityName;
            resultDto.CurrentConditions = results;

            return resultDto;
        }
    }

    private async Task<List<CurrentConditionDto>> GetCurrentConditions(JavaScriptSerializer js, WebClient client, WeatherLocationRootDto dto)
    {
        string currentConditionUrl = $"http://dataservice.accuweather.com/currentconditions/v1/{dto.Key}?apikey={apiKey}";
        string json = client.DownloadString(currentConditionUrl);
        List<CurrentConditionDto> list = js.Deserialize<List<CurrentConditionDto>>(json);

        foreach (var item in list)
        {
            item.Location = dto;
        }

        return list;
    }
}
