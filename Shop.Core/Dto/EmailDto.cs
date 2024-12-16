namespace Shop.Core.Dto;

using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

public class EmailDto
{
    public string Recipient { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsHtmlBody { get; set; } = false;
    public List<IFormFile> Files { get; set; }
}
