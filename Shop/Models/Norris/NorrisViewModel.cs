namespace Shop.Models.Norris;

using Microsoft.AspNetCore.Mvc.Rendering;

public class NorrisViewModel
{
    public string? Joke { get; set; }
    public string? Url { get; set; }

    public bool JokePresent { get; set; } = false;
    public string? Category { get; set; } = null;
    public List<SelectListItem> Categories { get; set; } = [];
}
