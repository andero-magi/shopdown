namespace Shop.Core.Dto;

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DbFilesHolder
{
    public List<IFormFile> Files { get; set; }
    public IEnumerable<FileToDbDto> Images { get; set; } = [];
}
