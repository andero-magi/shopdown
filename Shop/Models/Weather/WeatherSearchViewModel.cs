using Shop.Core.Dto.Weather;

namespace Shop.Models.Weather;


public class WeatherSearchViewModel
{
    public string SearchString { get; set; } = "";
    public LocationWeatherResultDto? Result { get; set; }
}
