namespace Shop.Controllers;

using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto.Weather;
using Shop.Core.ServiceInterface;
using Shop.Models.Weather;

public class WeatherController : Controller
{
    private readonly IWeatherForecastService _weather;

    public WeatherController(IWeatherForecastService weather)
    {
        _weather = weather;
    }

    public async Task<IActionResult> Index(WeatherSearchViewModel? vm)
    {
        string? search = null;

        if (vm != null)
        {
            search = vm.SearchString;
        }

        if (!string.IsNullOrEmpty(search))
        {
            var result = await _weather.GetWeather(search);
            vm = new WeatherSearchViewModel()
            {
                SearchString = search,
                Result = result
            };
        }

        return View(vm);
    }
}
