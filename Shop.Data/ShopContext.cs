namespace Shop.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto.Cocktails;

public class ShopContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Spaceship> Spaceships { get; set; }
    public DbSet<FileToApi> Files { get; set; }
    public DbSet<FileToDb> DbFiles { get; set; }
    public DbSet<RealEstate> RealEstate { get; set; }
    public DbSet<Drink> Drinks { get; set; }

    public DbSet<IdentityRole> IdentityRoles { get; set; }

    public ShopContext(DbContextOptions options)
        : base(options)
    {

    }
}
