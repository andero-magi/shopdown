namespace Shop.ApplicationServices.SpaceshipServices;

using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto;
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
    private readonly IFileService _fileService;

    public RealEstateService(ShopContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    async Task<RealEstate?> IRealEstateService.GetAsync(Guid? guid)
    {
        if (guid == null)
        {
            return null;
        }

        return await _context.RealEstate.FirstOrDefaultAsync(x => x.Id == guid);
    }

    async Task<RealEstate> IRealEstateService.Create(RealEstateDto dto)
    {
        RealEstate estate = new();
        dto.TransferTo(estate);

        estate.Id = Guid.NewGuid();
        estate.CreationTime = DateTime.Now;
        estate.ModifiedTime = DateTime.Now;

        _fileService.FilesToDb(dto, estate.Id);

        await _context.RealEstate.AddAsync(estate);
        await _context.SaveChangesAsync();

        return estate;
    }

    async Task<RealEstate?> IRealEstateService.Delete(Guid? guid)
    {
        RealEstate? found = await ((IRealEstateService) this).GetAsync(guid);
        if (found == null)
        {
            return null;
        }

        await _fileService.RemoveDbFiles((Guid) guid);
        _context.RealEstate.Remove(found);
        await _context.SaveChangesAsync();

        return found;
    }

    async Task<RealEstate> IRealEstateService.Update(RealEstateDto dto)
    {
        RealEstate estate = new();
        dto.TransferTo(estate);

        estate.ModifiedTime = DateTime.Now;

        _fileService.FilesToDb(dto, estate.Id);

        _context.RealEstate.Update(estate);
        await _context.SaveChangesAsync();

        return estate;
    }
}
