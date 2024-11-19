namespace Shop.Core.Dto.OpenWeather;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class OpenWeatherInfo
{
    [JsonPropertyName("location")]
    public OpenWeatherLocation Location { get; set; }

    [JsonPropertyName("current")]
    public OpenWeatherCurrent CurrentData { get; set; }
}
