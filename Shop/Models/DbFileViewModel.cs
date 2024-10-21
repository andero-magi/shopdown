using Shop.Core.Domain;
using Shop.Core.Dto;

namespace Shop.Models;

public class DbFileViewModel
{
    public Guid ImageId { get; set; }
    public string Title { get; set; }

    public DbFileViewModel()
    {

    }

    public DbFileViewModel(FileToDb db)
    {
        ImageId = db.Id;
        Title = db.ImageTitle;
    }

    public DbFileViewModel(FileToDbDto dto)
    {
        ImageId = dto.Id;
        Title = dto.ImageTitle;
    }
}
