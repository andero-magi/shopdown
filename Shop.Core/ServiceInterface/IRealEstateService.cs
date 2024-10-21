namespace Shop.Core.ServiceInterface;

using Shop.Core.Domain;
using Shop.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IRealEstateService
{
    Task<RealEstate?> GetAsync(Guid? guid);

    Task<RealEstate> Create(RealEstateDto dto);

    Task<RealEstate> Update(RealEstateDto dto);

    Task<RealEstate?> Delete(Guid? guid);
}
