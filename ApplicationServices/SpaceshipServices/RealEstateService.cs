namespace Shop.ApplicationServices.SpaceshipServices;

using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.ServiceInterface;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RealEstateService: IRealEstateService
{
    private readonly ShopContext _context;

    public RealEstateService(ShopContext context)
    {
        _context = context;
    }

    public async Task<RealEstate?> GetAsync(Guid? guid)
    {
        if (guid == null)
        {
            return null;
        }

        return await _context.RealEstate.FirstOrDefaultAsync(x => x.Id == guid);
    }
}
