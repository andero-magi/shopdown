using Shop.Utils;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models.Accounts;
public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    [ValidEmailDomain(".com", ErrorMessage = "Only .com email domains allowed")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare(nameof(Password), ErrorMessage = "Password and comfirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public string City { get; set; }
}
