namespace Shop.Controllers;

using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto.FreeGames;
using Shop.Core.ServiceInterface;

public class FreeGamesController : Controller
{
    private readonly IFreeGamesService _service;

    public FreeGamesController(IFreeGamesService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Query(FreeGameSearchDto? dto)
    {
        var list = await _service.QueryGames(dto);
        return Json(list);
    }
}
