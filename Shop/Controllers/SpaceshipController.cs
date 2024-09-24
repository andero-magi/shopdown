using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Spaceships;

namespace Shop.Controllers
{
    public class SpaceshipController : Controller
    {
        private readonly ShopContext _context;
        private readonly ISpaceshipServices _services;

        public SpaceshipController(ShopContext context, ISpaceshipServices services)
        {
            _context = context;
            _services = services;
        }

        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new SpaceshipIndexViewModel() { Ship = x })
                .ToList();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Spaceship? ship = await _services.DetailAsync((Guid)id);

            if (ship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipDetailViewModel();
            vm.Ship = ship;

            return View(vm);
        }
    }
}
