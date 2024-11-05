namespace Shop.Core.Dto.Weather;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class Measurement
{
    [JsonPropertyName("Metric")]
    public UnitValue Metric { get; set; }

    [JsonPropertyName("Imperial")]
    public UnitValue Imperial { get; set; }
}

public class UnitValue
{
    [JsonPropertyName("Value")]
    public int? Value { get; set; }

    [JsonPropertyName("Unit")]
    public string Unit { get; set; }

    [JsonPropertyName("UnitType")]
    public int? UnitType { get; set; }
}
