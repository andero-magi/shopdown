namespace Shop.ApplicationServices.SpaceshipServices;

using Shop.Core.Dto.OpenWeather;
using Shop.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OpenWeatherService : IOpenWeatherService
{
    private const string API_KEY = "f66a1929014f894d86294233e3d362b0";

    public Task<List<OpenWeatherInfo>> GetWeather(string cityName)
    {
        //
        // Steps:
        //  1. Lookup geo code
        //  2. Iterate over results
        //  3. Lookup current weather data for each result
        //
        string url = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit={limit}&appid={API key}"
    }
}
