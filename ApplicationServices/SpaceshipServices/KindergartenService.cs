using System;
using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;

namespace Shop.ApplicationServices.SpaceshipServices;

public class KindergartenService: IKindergartenService
{
    private readonly ShopContext _context;
    private readonly IFileService _files;

    public KindergartenService(ShopContext context, IFileService service)
    {
        _context = context;
        _files = service;
    }

    public async Task<Kindergarten> CreateAsync(KindergartenDto dto)
    {
        Kindergarten k = new();
        dto.TransferTo(k);

        k.Id = Guid.NewGuid();
        k.CreationDate = DateTime.Now;
        k.LastUpdateDate = DateTime.Now;

        _files.FilesToDb(dto, k.Id);

        await _context.Kindergartens.AddAsync(k);
        await _context.SaveChangesAsync();

        return k;
    }

    public async Task<Kindergarten?> DeleteAsync(Guid id)
    {
        var k = await GetKindergartenAsync(id);

        if (k == null) 
        {
            return null;
        }

        await _files.RemoveDbFiles(k.Id);

        _context.Kindergartens.Remove(k);
        await _context.SaveChangesAsync();

        return k;
    }

    public async Task<Kindergarten?> GetKindergartenAsync(Guid? guid)
    {
        if (guid == null) 
        {
            return null;
        }
        
        return await _context.Kindergartens.FirstOrDefaultAsync(x => x.Id == guid);
    }

    public async Task<Kindergarten> UpdateAsync(KindergartenDto dto)
    {
        Kindergarten k = new();

        dto.TransferTo(k);
        k.LastUpdateDate = DateTime.Now;

        _files.FilesToDb(dto, k.Id);

        _context.Kindergartens.Update(k);
        await _context.SaveChangesAsync();

        return k;
    }
}
