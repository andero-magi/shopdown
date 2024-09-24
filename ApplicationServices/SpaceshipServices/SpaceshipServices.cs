using Shop.Data;
using Shop.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Shop.ApplicationServices.SpaceshipServices
{
    public class SpaceshipServices: ISpaceshipServices
    {
        private readonly ShopContext _context;

        public SpaceshipServices(ShopContext context)
        {
            _context = context;
        }

        async Task<Spaceship?> ISpaceshipServices.DetailAsync(Guid spaceshipId)
        {
            return await _context.Spaceships.FirstOrDefaultAsync(x => x.Id == spaceshipId);
        }
    }
}
