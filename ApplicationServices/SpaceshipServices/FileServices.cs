using Microsoft.Extensions.Hosting;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.ApplicationServices.SpaceshipServices
{
    public class FileServices: IFileService
    {

        private readonly IHostEnvironment _webHost;
        private readonly ShopContext _context;

        public FileServices(IHostEnvironment webHost, ShopContext context)
        {
            _webHost = webHost;
            _context = context;
        }

        async Task IFileService.FilesToApi(SpaceshipDto dto, Spaceship spaceship)
        {
            var path = Path.Combine(_webHost.ContentRootPath, "/multipleFileUpload/");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var file in dto.Files)
            {
                String uniqueFileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                String filePath = Path.Combine(path, uniqueFileName);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fs);
                }

                FileToApi fileToApi = new()
                {
                    Id = Guid.NewGuid(),
                    ExistingFilePath = filePath,
                    SpaceshipId = spaceship.Id,
                };

                await _context.AddAsync(fileToApi);
            }

            await _context.SaveChangesAsync();
        }
    }
}
