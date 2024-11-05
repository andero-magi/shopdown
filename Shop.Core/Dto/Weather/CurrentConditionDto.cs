namespace Shop.Core.Dto.Weather;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class CurrentConditionDto
{
    public WeatherLocationRootDto? Location { get; set; }

    [JsonPropertyName("LocalObservationDateTime")]
    public DateTime LocalObservationDateTime { get; set; }

    [JsonPropertyName("EpochTime")]
    public int EpochTime { get; set; }

    [JsonPropertyName("WeatherText")]
    public string WeatherText { get; set; }

    [JsonPropertyName("WeatherIcon")]
    public int WeatherIcon { get; set; }

    [JsonPropertyName("HasPrecipitation")]
    public bool HasPrecipitation { get; set; }

    [JsonPropertyName("PrecipitationType")]
    public object PrecipitationType { get; set; }

    [JsonPropertyName("IsDayTime")]
    public bool IsDayTime { get; set; }

    [JsonPropertyName("Temperature")]
    public Measurement Temperature { get; set; }

    [JsonPropertyName("MobileLink")]
    public string MobileLink { get; set; }

    [JsonPropertyName("Link")]
    public string Link { get; set; }
}
