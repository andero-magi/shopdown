namespace Shop.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Models.RealEstate;

public class RealEstateController : Controller
{
    private ShopContext _context;
    private IRealEstateService _service;

    public RealEstateController(ShopContext ctx, IRealEstateService service)
    {
        _context = ctx;
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        IEnumerable<RealEstate> res = await _context.RealEstate.ToListAsync();
        return View(res);
    }

    public IActionResult Create()
    {
        RealEstateUpdateViewModel vm = new()
        {
            Dto = new RealEstateDto()
        };

        ViewData["IsCreate"] = "true";
        return View("CreateUpdate", vm);
    }

    [HttpPost]
    [ActionName("Create")]
    public async Task<IActionResult> Create(RealEstateUpdateViewModel? vm)
    {
        if (vm == null)
        {
            return NotFound();
        }

        await _service.Create(vm.Dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(Guid? id)
    {
        RealEstate? estate = await _service.GetAsync(id);

        if (estate == null)
        {
            return NotFound();
        }

        var vm = new RealEstateUpdateViewModel
        {
            Dto = new RealEstateDto(estate)
        };

        ViewData["IsCreate"] = "false";
        return View("CreateUpdate", vm);
    }

    [HttpPost]
    [ActionName("Update")]
    public async Task<IActionResult> Update(RealEstateUpdateViewModel? vm)
    {
        if (vm == null)
        {
            return NotFound();
        }

        await _service.Update(vm.Dto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(Guid? id)
    {
        RealEstate? found = await _service.GetAsync(id);
        if (found == null)
        {
            return NotFound();
        }

        RealEstateDeleteViewModel vm = new()
        {
            Estate = found
        };

        return View(vm);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> Delete(RealEstateDeleteViewModel? vm)
    {
        if (vm == null)
        {
            return NotFound();
        }

        await _service.Delete(vm.Estate.Id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        RealEstate? estate = await _service.GetAsync(id);

        if (estate == null)
        {
            return NotFound();
        }

        return View(estate);
    }
}
