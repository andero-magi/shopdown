using Shop.Core.Domain;
using Shop.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface ISpaceshipServices
    {
        public Task<Spaceship?> GetShipAsync(Guid spaceshipId);

        public Task<Spaceship> UpdateAsync(SpaceshipDto spaceship);
    }
}
