namespace Shop.Core.Dto;

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
}
