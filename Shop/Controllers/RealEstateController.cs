namespace Shop.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Core.Domain;

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
}
