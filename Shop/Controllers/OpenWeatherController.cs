namespace Shop.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto.OpenWeather;
using Shop.Core.ServiceInterface;

[Authorize]
public class OpenWeatherController : Controller
{
    private readonly IOpenWeatherService _service;

    public OpenWeatherController(IOpenWeatherService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Query([FromQuery] string? q) 
    {
        if (string.IsNullOrEmpty(q)) 
        {
            return NotFound();
        }

        List<OpenWeatherInfo> list = await _service.GetWeather(q);
        return Json(list);
    }
}
