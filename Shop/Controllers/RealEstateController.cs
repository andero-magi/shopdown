namespace Shop.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Models.RealEstate;
using Microsoft.AspNetCore.StaticFiles;

public class RealEstateController : Controller
{
    private readonly ShopContext _context;
    private readonly IRealEstateService _service;
    private readonly IFileService _fileService;

    public RealEstateController(ShopContext ctx, IRealEstateService service, IFileService fileService)
    {
        _context = ctx;
        _service = service;
        _fileService = fileService;
    }

    [Route("RealEstate/asset/{imageId}")]
    [ActionName("asset")]
    public async Task<IActionResult> GetFile([FromRoute] Guid imageId)
    {
        var file = await _fileService.GetDatabaseFile(imageId);
        if (file == null)
        {
            return NotFound();
        }

        new FileExtensionContentTypeProvider().TryGetContentType(file.ImageTitle, out string? contentType);

        return File(file.ImageData, contentType);
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

        vm.Dto.Files = [];
        vm.Dto.Images = [];

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

    public async Task<IActionResult> RemoveImage(Guid? imageId)
    {
        if (imageId == null)
        {
            return NotFound();
        }

        var removed = await _fileService.RemoveImageById((Guid) imageId);
        if (removed == null)
        {
            return NotFound();
        }
        if (removed.RealEstateId == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Update), new { id = removed.RealEstateId });
    }

    public async Task<IActionResult> RemoveAllImages(Guid? guid)
    {
        var estate = await _service.GetAsync(guid);
        if (estate == null)
        {
            return NotFound();
        }

        await _fileService.RemoveDbFiles(estate.Id);
        return RedirectToAction(nameof(Index));
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

        vm.Dto.Files = [];
        vm.Dto.Images = (await _fileService.GetDatabaseFiles(estate.Id))
            .Select(x => new FileToDbDto(x));
        
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
            Estate = found,
            Files = await _fileService.GetDatabaseFiles((Guid) id)
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

        List<FileToDb> images = await _fileService.GetDatabaseFiles((Guid) id);

        var vm = new RealEstateDetailsViewModel()
        {
            Estate = estate,
            Files = images
        };

        return View(vm);
    }
}
