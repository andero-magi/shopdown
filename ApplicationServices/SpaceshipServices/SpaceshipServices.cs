using Shop.Data;
using Shop.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Dto;

namespace Shop.ApplicationServices.SpaceshipServices
{
    public class SpaceshipServices: ISpaceshipServices
    {
        private readonly ShopContext _context;

        public SpaceshipServices(ShopContext context)
        {
            _context = context;
        }

        async Task<Spaceship?> ISpaceshipServices.GetShipAsync(Guid spaceshipId)
        {
            return await _context.Spaceships.FirstOrDefaultAsync(x => x.Id == spaceshipId);
        }

        async Task<Spaceship> ISpaceshipServices.UpdateAsync(SpaceshipDto spaceship)
        {
            Spaceship domain = new Spaceship();
            
            domain.Id = spaceship.Id;
            domain.Name = spaceship.Name;
            domain.Typename = spaceship.Typename;
            domain.SpaceshipModel = spaceship.SpaceshipModel;
            domain.BuildDate = spaceship.BuildDate;
            domain.Crew = spaceship.Crew;
            domain.EnginePower = spaceship.EnginePower;
            domain.CreatedAt = spaceship.CreatedAt;
            domain.LastUpdatedAt = spaceship.LastUpdatedAt;

            _context.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
    }
}
