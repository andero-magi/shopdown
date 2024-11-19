using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto.Cocktails;

namespace Shop.Data
{
    public class ShopContext: DbContext
    {
        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<FileToApi> Files { get; set; }
        public DbSet<FileToDb> DbFiles { get; set; }
        public DbSet<RealEstate> RealEstate { get; set; }
        public DbSet<Drink> Drinks { get; set; }

        public ShopContext(DbContextOptions options)
            : base(options)
        {
            
        }
    }
}
