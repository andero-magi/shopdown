namespace Shop.Controllers;

using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Models.Email;

public class EmailController : Controller
{
    private readonly IEmailService _service;

    public EmailController(IEmailService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View(new EmailViewModel() { Dto = new()});
    }

    [HttpPost]
    public async Task<IActionResult> Send(EmailViewModel vm)
    {
        await _service.SendEmail(vm.Dto);
        return View("Sent", vm);
    }
}
