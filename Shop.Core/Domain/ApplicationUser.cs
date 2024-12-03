namespace Shop.Core.Domain;

using Microsoft.AspNetCore.Identity;

public class ApplicationUser: IdentityUser
{
    public string City { get; set; }

}
