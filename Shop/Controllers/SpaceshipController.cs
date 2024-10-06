using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models;
using Shop.Models.Spaceships;

namespace Shop.Controllers
{
    public class SpaceshipController : Controller
    {
        private readonly ShopContext _context;
        private readonly ISpaceshipServices _services;
        private readonly ILogger<SpaceshipController> _logger;

        public SpaceshipController(ShopContext context, ISpaceshipServices services, ILogger<SpaceshipController> logger)
        {
            _context = context;
            _services = services;
            _logger = logger;
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

            var vm = new SpaceshipDetailViewModel
            {
                Ship = ship,
                Images = await GetImageViewModelsAsync(ship.Id)
            };

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

            SpaceshipCreateUpdateViewModel vm = new()
            {
                Dto = new SpaceshipDto(ship),
                Images = await GetImageViewModelsAsync(ship.Id)
            };

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

            var vm = new SpaceshipDeleteViewModel
            {
                Ship = ship,
                Images = await GetImageViewModelsAsync(ship.Id)
            };

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
            SpaceshipCreateUpdateViewModel vm = new()
            {
                Dto = new SpaceshipDto(),
                Images = []
            };

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

        private async Task<List<ImageViewModel>> GetImageViewModelsAsync(Guid spaceshipId) 
        {
            return await _context.Files
                .Where(x => x.SpaceshipId == spaceshipId)
                .Select(y => new ImageViewModel 
                {
                    ImageId = y.Id,
                    FilePath = y.ExistingFilePath
                }).ToListAsync();
        }
    }
}
