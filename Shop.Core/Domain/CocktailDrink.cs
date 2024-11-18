namespace Shop.Core.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class CocktailDrink
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; }

    [JsonPropertyName("iba")]
    public string IBA { get; set; }

    [JsonPropertyName("alcaholic")]
    public string Alcaholic { get; set; }

    [JsonPropertyName("glass")]
    public string Glass { get; set; }

    [JsonPropertyName("instructions_en")]
    public string InstructionsEn { get; set; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = [];

    [JsonPropertyName("ingredients")]
    public List<Ingredient> Ingredients { get; set; } = [];
}

public class Ingredient
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("measurement")]
    public string Measurement { get; set; }
}