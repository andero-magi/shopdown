namespace Shop.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Models.Accounts;
using Shop.Models.RealEstate;

public class AccountsController : Controller
{
    private readonly UserManager<ApplicationUser> _manager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailService _emails;

    public AccountsController(
        UserManager<ApplicationUser> manager, 
        SignInManager<ApplicationUser> signInManager,
        IEmailService emails)
    {
        _manager = manager;
        _signInManager = signInManager;
        _emails = emails;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail([FromQuery] Guid? userId, [FromQuery] string? token)
    {
        if (userId == null || string.IsNullOrEmpty(token))
        {
            return BadRequest();
        }

        var user = await _manager.FindByIdAsync(userId.ToString());
        var result = await _manager.ConfirmEmailAsync(user, token);

        await _signInManager.SignInAsync(user, true);

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

                EmailDto email = new()
                {
                    Recipient = vm.Email,
                    Subject = "Confirm Email",
                    IsHtmlBody = true,
                    Body = $"""
                    Thank you for signing up to the website!
                        
                    <p>
                      Please confirm your email by clicking <a href="{confirmationLink}">here.</a>
                    </p>
                    <p>
                      - Shop CRUD
                    </p>
                    """
                };

                await _emails.SendEmail(email);

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
        if (ModelState.IsValid)
        {
            var user = await _manager.FindByEmailAsync(vm.Email);

            if (user != null && !user.EmailConfirmed && (await _manager.CheckPasswordAsync(user, vm.Password)))
            {
                ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, false);

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

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult ForgotPassword()
    {
        return View(new ForgotPasswordViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var user = await _manager.FindByEmailAsync(vm.Email);

            if (user != null && (await _manager.IsEmailConfirmedAsync(user)))
            {
                var token = await _manager.GeneratePasswordResetTokenAsync(user);
                var link = Url.Action("ResetPassword", "Accounts", new { userId = user.Id, token }, Request.Scheme);

                EmailDto dto = new()
                {
                    Recipient = user.Email,
                    Subject = "Password reset",
                    IsHtmlBody = true,
                    Body = $"""
                    Password reset link:
                    <a href="{link}">Click here to reset password</a>
                    """
                };

                await _emails.SendEmail(dto);

                return View("ForgotPasswordConfirm");
            }

            return View("ForgotPasswordConfirm");
        }

        return View(vm);
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View(new ChangePasswordViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var user = await _manager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await _manager.ChangePasswordAsync(user, vm.CurrentPassword, vm.NewPassword);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
        }

        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var user = await _manager.FindByIdAsync(vm.UserId.ToString());
        if (user == null)
        {
            return RedirectToAction("Index", "Home");
        }

        var result = await _manager.ResetPasswordAsync(user, vm.Token, vm.ConfirmPassword);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(vm);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(Guid userId, string token)
    {
        ResetPasswordViewModel vm = new()
        {
            UserId = userId,
            Token = token
        };

        return View(vm);
    }
}
