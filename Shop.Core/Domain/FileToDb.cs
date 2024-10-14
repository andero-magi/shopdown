namespace Shop.Core.Domain;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FileToDb
{
    public Guid Id { get; set; }
    public string ImageTitle { get; set; }
    public byte[] ImageData { get; set; }
    public Guid? RealEstateId { get; set; }
}
