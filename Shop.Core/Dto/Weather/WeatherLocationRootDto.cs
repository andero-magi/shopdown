namespace Shop.Core.Dto.Weather;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class AdministrativeArea
{
    [JsonPropertyName("ID")]
    public string ID { get; set; }

    [JsonPropertyName("LocalizedName")]
    public string LocalizedName { get; set; }

    [JsonPropertyName("EnglishName")]
    public string EnglishName { get; set; }

    [JsonPropertyName("Level")]
    public int? Level { get; set; }

    [JsonPropertyName("LocalizedType")]
    public string LocalizedType { get; set; }

    [JsonPropertyName("EnglishType")]
    public string EnglishType { get; set; }

    [JsonPropertyName("CountryID")]
    public string CountryID { get; set; }
}

public class Country
{
    [JsonPropertyName("ID")]
    public string ID { get; set; }

    [JsonPropertyName("LocalizedName")]
    public string LocalizedName { get; set; }

    [JsonPropertyName("EnglishName")]
    public string EnglishName { get; set; }
}

public class GeoPosition
{
    [JsonPropertyName("Latitude")]
    public double? Latitude { get; set; }

    [JsonPropertyName("Longitude")]
    public double? Longitude { get; set; }

    [JsonPropertyName("Elevation")]
    public Measurement Elevation { get; set; }
}

public class Region
{
    [JsonPropertyName("ID")]
    public string ID { get; set; }

    [JsonPropertyName("LocalizedName")]
    public string LocalizedName { get; set; }

    [JsonPropertyName("EnglishName")]
    public string EnglishName { get; set; }
}

public class SupplementalAdminArea
{
    [JsonPropertyName("Level")]
    public int? Level { get; set; }

    [JsonPropertyName("LocalizedName")]
    public string LocalizedName { get; set; }

    [JsonPropertyName("EnglishName")]
    public string EnglishName { get; set; }
}

public class TimeZone
{
    [JsonPropertyName("Code")]
    public string Code { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("GmtOffset")]
    public int? GmtOffset { get; set; }

    [JsonPropertyName("IsDaylightSaving")]
    public bool? IsDaylightSaving { get; set; }

    [JsonPropertyName("NextOffsetChange")]
    public DateTime? NextOffsetChange { get; set; }
}

public class WeatherLocationRootDto
{
    [JsonPropertyName("Version")]
    public int? Version { get; set; }

    [JsonPropertyName("Key")]
    public string Key { get; set; }

    [JsonPropertyName("Type")]
    public string Type { get; set; }

    [JsonPropertyName("Rank")]
    public int? Rank { get; set; }

    [JsonPropertyName("LocalizedName")]
    public string LocalizedName { get; set; }

    [JsonPropertyName("EnglishName")]
    public string EnglishName { get; set; }

    [JsonPropertyName("PrimaryPostalCode")]
    public string PrimaryPostalCode { get; set; }

    [JsonPropertyName("Region")]
    public Region Region { get; set; }

    [JsonPropertyName("Country")]
    public Country Country { get; set; }

    [JsonPropertyName("AdministrativeArea")]
    public AdministrativeArea AdministrativeArea { get; set; }

    [JsonPropertyName("TimeZone")]
    public TimeZone TimeZone { get; set; }

    [JsonPropertyName("GeoPosition")]
    public GeoPosition GeoPosition { get; set; }

    [JsonPropertyName("IsAlias")]
    public bool? IsAlias { get; set; }

    [JsonPropertyName("SupplementalAdminAreas")]
    public List<SupplementalAdminArea> SupplementalAdminAreas { get; set; }

    [JsonPropertyName("DataSets")]
    public List<string> DataSets { get; set; }
}