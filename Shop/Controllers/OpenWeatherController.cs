namespace Shop.Controllers;

using Microsoft.AspNetCore.Mvc;

public class OpenWeatherController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
