using Microsoft.AspNetCore.Mvc;
<<<<<<< Updated upstream
using Shop.Data;
=======
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Kindergartens;
>>>>>>> Stashed changes

namespace Shop.Controllers;

public class KindergartenController : Controller
{
    private readonly ShopContext _context;
<<<<<<< Updated upstream

    public KindergartenController(ShopContext context)
    {
        _context = context;
    }

    public IActionResult Index() 
    {
        return View();
=======
    private readonly IKindergartenService _kindergartens;

    public KindergartenController(ShopContext context, IKindergartenService service)
    {
        _context = context;
        _kindergartens = service;
    }

    public async Task<IActionResult> Index() 
    {
        IEnumerable<KindergartenIndexViewModel> model = await _context.Kindergartens
            .Select(x => new KindergartenIndexViewModel() {Kindergarten = x})
            .ToListAsync();

        return View(model);
    }

    public async Task<IActionResult> Delete(Guid? guid)
    {
        var kindergarten = await _kindergartens.GetKindergartenAsync(guid);
        if (kindergarten == null)
        {
            return NotFound();
        }

        KindergartenDeleteViewModel vm = new()
        {
            Kindergarten = kindergarten
        };

        return View(vm);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> ConfirmDelete(KindergartenDeleteViewModel? vm) 
    {
        if (vm == null)
        {
            return NotFound();
        }

        await _kindergartens.DeleteAsync(vm.Kindergarten.Id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(Guid? guid) 
    {
        var kindergarten = await _kindergartens.GetKindergartenAsync((Guid) guid);
        if (kindergarten == null)
        {
            return NotFound();
        }

        return View(new KindergartenDetailsViewModel() {Kindergarten = kindergarten});
    }

    public async Task<IActionResult> Update(Guid? guid) 
    {
        Kindergarten? kindergarten = await _kindergartens.GetKindergartenAsync((Guid) guid);
        if (kindergarten == null) 
        {
            return NotFound();
        }

        ViewData["isCreate"] = "false";

        KindergartenUpdateViewModel vm = new()
        {
            Dto = new KindergartenDto(kindergarten)
        };

        return View("CreateUpdate", vm);
    }

    [HttpPost]
    [ActionName("Update")]
    public async Task<IActionResult> Update(KindergartenUpdateViewModel? vm)
    {
        if (vm == null)
        {
            return NotFound();
        }

        await _kindergartens.UpdateAsync(vm.Dto);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create()
    {
        KindergartenUpdateViewModel vm = new()
        {
            Dto = new()
        };

        ViewData["isCreate"] = "true";
        return View("CreateUpdate", vm);
    }

    [HttpPost]
    [ActionName("Create")]
    public async Task<IActionResult> Create(KindergartenUpdateViewModel? vm)
    {
        if (vm == null) 
        {
            return NotFound();
        }

        await _kindergartens.CreateAsync(vm.Dto);
        return RedirectToAction(nameof(Index));
>>>>>>> Stashed changes
    }
}
