namespace Shop.ApplicationServices.SpaceshipServices;

using Accuweather;
using Nancy.Json;
using Shop.Core.Dto.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WeatherForecastService
{
    private readonly IAccuweatherApi _accuWeather;

    public WeatherForecastService()
    {
        string? apiKey = Environment.GetEnvironmentVariable("ACCU_WEATHER_API_KEY");
        _accuWeather = new AccuweatherApi(apiKey, "en-us");
    }

    async Task<LocationWeatherResultDto> GetWeather(LocationWeatherResultDto dto)
    {
        string json = await _accuWeather.Locations.CitySearch("Tallinn");
        List<WeatherLocationRootDto> result = new JavaScriptSerializer().Deserialize<List<WeatherLocationRootDto>>(json);
        

    }
}
