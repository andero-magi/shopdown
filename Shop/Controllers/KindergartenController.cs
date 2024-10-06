using Microsoft.AspNetCore.Mvc;
using Shop.Data;

namespace Shop.Controllers;

public class KindergartenController : Controller
{
    private readonly ShopContext _context;

    public KindergartenController(ShopContext context)
    {
        _context = context;
    }

    public IActionResult Index() 
    {
        return View();
    }
}
