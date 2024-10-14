namespace Shop.Core.Dto;

using Microsoft.AspNetCore.Http;
using Shop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class RealEstateDto
{
    public Guid? Id { get; set; }
    public double? Size { get; set; }
    public int? RoomNumber { get; set; }
    public string? BuildingType { get; set; }

    public List<IFormFile> Files { get; set; }
    public IEnumerable<FileToDbDto> Images { get; set; } = [];

    public RealEstateDto()
    {
        
    }

    public RealEstateDto(RealEstate estate)
    {
        Id = estate.Id;
        Size = estate.Size;
        RoomNumber = estate.RoomNumber;
        BuildingType = estate.BuildingType;
    }

    public void TransferTo(RealEstate estate)
    {
        estate.Id = Id ?? Guid.Empty;
        estate.Size = Size ?? 0.0d;
        estate.RoomNumber = RoomNumber ?? 0;
        estate.BuildingType = BuildingType!;
    }
}
