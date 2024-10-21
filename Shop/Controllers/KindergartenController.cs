using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.ApplicationServices.SpaceshipServices;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.Kindergartens;

namespace Shop.Controllers;

public class KindergartenController : Controller
{
    private readonly ShopContext _context;
    private readonly IKindergartenService _kindergartens;
    private readonly IFileService _files;

    public KindergartenController(ShopContext context, IKindergartenService service, IFileService files)
    {
        _context = context;
        _kindergartens = service;
        _files = files;
    }

    public async Task<IActionResult> Index() 
    {
        IEnumerable<KindergartenIndexViewModel> model = await _context.Kindergartens
            .Select(x => new KindergartenIndexViewModel() {Kindergarten = x})
            .ToListAsync();

        return View(model);
    }

    public async Task<IActionResult> RemoveAllImages(Guid? guid)
    {
        var kindergarten = await _kindergartens.GetKindergartenAsync(guid);
        if (kindergarten == null)
        {
            return NotFound();
        }

        await _files.RemoveDbFiles(kindergarten.Id);
        return RedirectToAction(nameof(Update), new { guid = kindergarten.Id });
    }

    public async Task<IActionResult> RemoveImage(Guid? imageId)
    {
        if (imageId == null)
        {
            return NotFound();
        }

        var removed = await _files.RemoveImageById((Guid)imageId);
        if (removed == null)
        {
            return NotFound();
        }
        if (removed.HolderId == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Update), new { guid = removed.HolderId });
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
            Kindergarten = kindergarten,
            Images = await _files.GetDatabaseFiles(kindergarten.Id)
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

        var vm = new KindergartenDetailsViewModel() 
        { 
            Kindergarten = kindergarten,
            Images = await _files.GetDatabaseFiles(kindergarten.Id)
        };

        return View(vm);
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
            Dto = new KindergartenDto(kindergarten),
            Images = await _files.GetDatabaseFiles(kindergarten.Id)
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
    }
}
