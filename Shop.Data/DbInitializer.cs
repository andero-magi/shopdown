using Shop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data
{
    public class DbInitializer
    {

        public static void InitDb(ShopContext context)
        {
            if (!context.Spaceships.Any())
            {
                var ships = new Spaceship[]
                {
                    new()
                    {
                        Name = "Cargos",
                        Typename = "Cargo",
                        SpaceshipModel = "Small Cargo",
                        EnginePower = 250,
                        Crew = 4,
                        CreatedAt = DateTime.Now,
                        LastUpdatedAt = DateTime.Now,
                        BuildDate = DateTime.Now.AddYears(20),
                    },
                    new()
                    {
                        Name = "Freight",
                        Typename = "FreigthShip",
                        SpaceshipModel = "Large Freigther",
                        EnginePower = 270,
                        Crew = 120,
                        CreatedAt = DateTime.Now,
                        LastUpdatedAt = DateTime.Now,
                        BuildDate = DateTime.Now.AddYears(40),
                    },
                };

                context.Spaceships.AddRange(ships);
                context.SaveChanges();
            }

            if (!context.Kindergartens.Any()) 
            {
                var random = new Random();

                var kindergartens = new Kindergarten[] 
                {
                    new() 
                    {
                        Id = Guid.NewGuid(),
                        GroupName = "Foo",
                        ChildrenCount = random.Next(5, 24),
                        KindergartenName = "KinderEgg",
                        Teacher = "Mr. Roswell Incident",
                        CreationDate = DateTime.Now.AddYears(-3),
                        LastUpdateDate = DateTime.Now.AddDays(2)
                    },
                    new() 
                    {
                        Id = Guid.NewGuid(),
                        GroupName = "BarFoo",
                        ChildrenCount = random.Next(5, 24),
                        KindergartenName = "Kinder Surprise Egg",
                        Teacher = "King Ludwig II, of Bavaria",
                        CreationDate = new DateTime(1869, 9, 5),
                        LastUpdateDate = DateTime.Now.AddDays(-2)
                    }
                };

                context.Kindergartens.AddRange(kindergartens);
                context.SaveChanges();
            }
        }
    }
}
