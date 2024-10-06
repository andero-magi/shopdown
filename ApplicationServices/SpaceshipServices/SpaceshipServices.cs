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
        private readonly IFileService _files;

        public SpaceshipServices(ShopContext context, IFileService files)
        {
            _context = context;
            _files = files;
        }

        async Task<Spaceship?> ISpaceshipServices.GetShipAsync(Guid spaceshipId)
        {
            return await _context.Spaceships.FirstOrDefaultAsync(x => x.Id == spaceshipId);
        }

        async Task<Spaceship> ISpaceshipServices.UpdateAsync(SpaceshipDto spaceship)
        {
            Spaceship domain = new Spaceship();
            spaceship.TransferTo(domain);

            _files.FilesToApi(spaceship, domain);

            _context.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        async Task<Spaceship?> ISpaceshipServices.Delete(Guid guid)
        {
            Spaceship? ship = await ((ISpaceshipServices)this).GetShipAsync(guid);
            if (ship == null)
            {
                return null;
            }

            var images = await _context.Files
                .Where(x => x.SpaceshipId == guid)
                .Select(y => new FileToApiDto
                {
                    Id = y.Id,
                    SpaceshipId = y.SpaceshipId,
                    ExistingFilePath = y.ExistingFilePath
                }).ToArrayAsync();

            await _files.RemoveImagesFromApi(images);

            _context.Remove(ship);
            await _context.SaveChangesAsync();

            return ship;
        }

        async Task<Spaceship> ISpaceshipServices.Create(SpaceshipDto dto)
        {
            Spaceship ship = new();
            dto.TransferTo(ship);
            ship.CreatedAt = DateTime.Now;
            ship.LastUpdatedAt = DateTime.Now;

            _files.FilesToApi(dto, ship);

            await _context.AddAsync(ship);
            await _context.SaveChangesAsync();

            return ship;
        }
    }
}
