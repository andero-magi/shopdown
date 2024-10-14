using Shop.Core.Domain;

namespace Shop.Models.RealEstate;

public class RealEstateDetailsViewModel
{
    public Shop.Core.Domain.RealEstate Estate { get; set; }
    public List<FileToDb> Files { get; set; }
}
