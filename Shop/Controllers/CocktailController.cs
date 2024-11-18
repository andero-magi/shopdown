using Microsoft.AspNetCore.Mvc;

namespace Shop.Controllers;

public class CocktailController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
