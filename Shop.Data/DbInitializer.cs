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

            if (!context.RealEstate.Any())
            {
                var estaes = new RealEstate[]
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Size = 14.0d,
                        RoomNumber = 0,
                        BuildingType = "Apartment"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Size = 28.0d,
                        RoomNumber = 14,
                        BuildingType = "Warehouse"
                    }
                };

                context.RealEstate.AddRange(estaes);
                context.SaveChanges();
            }
        }
    }
}
