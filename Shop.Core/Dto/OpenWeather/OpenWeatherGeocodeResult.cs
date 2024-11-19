using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shop.Core.Dto.OpenWeather;

public class OpenWeatherGeocodeResult
{
    public List<OpenWeatherLocation>? Locations { get; set; }
}

public class OpenWeatherLocation
{

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("local_names")]
    public Dictionary<string, string> LocalNames { get; set; }

    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    [JsonPropertyName("lon")]
    public double Lon { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }
}