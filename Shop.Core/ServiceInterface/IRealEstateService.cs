namespace Shop.Core.ServiceInterface;

using Shop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IRealEstateService
{
    Task<RealEstate?> GetAsync(Guid? guid);
}
