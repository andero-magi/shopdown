using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;

namespace Shop.ApplicationServices.SpaceshipServices
{
    public class FileServices: IFileService
    {
        public static readonly string DIR_NAME = "multipleFileUpload";

        private readonly IHostEnvironment _webHost;
        private readonly ShopContext _context;

        public FileServices(IHostEnvironment webHost, ShopContext context)
        {
            _webHost = webHost;
            _context = context;
        }

        public static string EnsureDirectoryExists(string rootPath) {
            var path = GetPath(rootPath);

            if (Directory.Exists(path)) {
                return path;
            }

            Directory.CreateDirectory(path);
            return path;
        }

        public static string GetPath(string rootPath) {
            return Path.Combine(rootPath, DIR_NAME + "/");
        }

        async void IFileService.FilesToApi(SpaceshipDto dto, Spaceship spaceship)
        {
            var path = EnsureDirectoryExists(_webHost.ContentRootPath);

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

                _context.Add(fileToApi);
            }
        }

        async Task<List<FileToApi>?> IFileService.RemoveImagesFromApi(FileToApiDto[] dtos) 
        {
            foreach (var dto in dtos)
            {
                FileToApi? imageId = await _context.Files
                    .FirstOrDefaultAsync(x => x.ExistingFilePath == dto.ExistingFilePath);

                if (imageId == null) 
                {
                    continue;
                }

                string filePath = Path.Combine(GetPath(_webHost.ContentRootPath), imageId.ExistingFilePath);

                if (File.Exists(filePath)) 
                {
                    File.Delete(filePath);
                }

                _context.Files.Remove(imageId);
            }

            await _context.SaveChangesAsync();
            return null;
        }
    }
}
