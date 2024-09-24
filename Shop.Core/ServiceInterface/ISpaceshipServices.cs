using Shop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.ServiceInterface
{
    public interface ISpaceshipServices
    {
        public Task<Spaceship?> DetailAsync(Guid spaceshipId);
    }
}
