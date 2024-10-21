namespace Shop.Core.Dto;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FileToDbDto
{
    public Guid Id { get; set; }
    public string ImageTitle { get; set; }
    public byte[] ImageData { get; set; }
    public Guid? HolderId { get; set; }

    public FileToDbDto()
    {
        
    }

    public FileToDbDto(FileToDb db)
    {
        Id = db.Id;
        ImageTitle = db.ImageTitle;
        ImageData = db.ImageData;
        HolderId = db.HolderId;
    }
}
