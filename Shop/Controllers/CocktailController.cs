namespace Shop.Controllers;

using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Core.Dto.Cocktails;
using Shop.Core.ServiceInterface;
using Shop.Data;

public class CocktailController : Controller
{
    private readonly ICocktailService _service;

    public CocktailController(ICocktailService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }


    public async Task<IActionResult> Query([FromQuery] string query)
    {
        var list = await _service.GetCocktails(query);
        return Json(list);
    }

    public async Task<IActionResult> Saved()
    {
        var list = await _service.GetSaved();
        return Json(list);
    }

    [HttpPost]
    public async Task<IActionResult> Save(string id)
    { 
        CocktailDto dto = await _service.GetById(id);
        if (dto == null)
        {
            return NotFound();
        }

        await _service.SaveToDatabase(dto);

        return Ok();
    }
}
