namespace Shop.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Models.Accounts;

public class AccountsController : Controller
{
    private readonly UserManager<ApplicationUser> _manager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountsController(UserManager<ApplicationUser> manager, SignInManager<ApplicationUser> signInManager)
    {
        _manager = manager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var ser = new ApplicationUser()
            {
                UserName = vm.Email,
                Email = vm.Email,
                City = vm.City,
            };

            var result = await _manager.CreateAsync(ser, vm.Password);

            if (result.Succeeded)
            {
                var token = await _manager.GenerateEmailConfirmationTokenAsync(ser);
                var confirmationLink = Url.Action("ConfirmEmail", "Accounts", new {userId = ser.Id, token = token}, Request.Scheme);

                if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                {
                    return RedirectToAction("ListUsers", "Admin");
                }

                ViewBag.ErrorTitle = "Registration successful";
                ViewBag.ErrorMessage = "Before you can log in, please confirm your email address.";

                return View("Error");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View("Error");
    }
}
