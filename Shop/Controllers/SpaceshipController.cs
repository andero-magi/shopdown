using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Core.Dto;
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

            Spaceship? ship = await _services.GetShipAsync((Guid)id);

            if (ship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipDetailViewModel();
            vm.Ship = ship;

            return View(vm);
        }

        public async Task<IActionResult> Update(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ship = await _services.GetShipAsync((Guid)id);

            if (ship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipCreateUpdateViewModel();
            vm.Dto = new SpaceshipDto(ship);

            ViewData["Title"] = "Update";
            return View("CreateUpdate", vm);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateShip(SpaceshipCreateUpdateViewModel? vm)
        {
            if (vm == null)
            {
                return NotFound();
            }

            vm.Dto.LastUpdatedAt = DateTime.Now;

            var result = await _services.UpdateAsync(vm.Dto);
            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Update), vm.Dto.Id);
        }

        public async Task<IActionResult> Delete(Guid? guid)
        {
            if (guid == null)
            {
                return NotFound();
            }

            var ship = await _services.GetShipAsync((Guid)guid);
            if (ship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipDeleteViewModel();
            vm.Ship = ship;

            return View(vm);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _services.Delete((Guid)id);
            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Delete));
        }

        [HttpGet]
        public IActionResult Create()
        {
            SpaceshipCreateUpdateViewModel vm = new();
            vm.Dto = new SpaceshipDto();
            ViewData["Title"] = "Create";
            return View("CreateUpdate", vm);
        }

        public async Task<IActionResult> Create(SpaceshipCreateUpdateViewModel? vm)
        {
            if (vm == null)
            {
                return NotFound();
            }

            var result = await _services.Create(vm.Dto);
            return RedirectToAction(nameof(Index));
        }
    }
}
