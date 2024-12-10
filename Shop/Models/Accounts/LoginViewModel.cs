namespace Shop.Models.Accounts;

using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }

    public LoginType? Type { get; set; }

    public IList<AuthenticationScheme>? ExternalLogins { get; set; }

}

public enum LoginType
{
    Regular,
    Admin
}