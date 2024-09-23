using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models.Spaceships;

namespace Shop.Controllers
{
    public class SpaceshipController : Controller
    {
        private readonly ShopContext _context;

        public SpaceshipController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new SpaceshipIndexViewModel() { Ship = x })
                .ToList();

            return View(result);
        }
    }
}
