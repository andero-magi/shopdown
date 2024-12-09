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
                UserName = vm.Username,
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

                return View("EmailError");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string? returnUrl)
    {
        LoginViewModel vm = new()
        {
            ReturnUrl = returnUrl,
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
        };

        return View(vm);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel vm, string? returnUrl)
    {
        if (!ModelState.IsValid)
        {
            var user = await _manager.FindByEmailAsync(vm.Email);

            if (user != null && !user.EmailConfirmed && (await _manager.CheckPasswordAsync(user, vm.Password)))
            {
                ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                return View("AccountLocked");
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
        }

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
