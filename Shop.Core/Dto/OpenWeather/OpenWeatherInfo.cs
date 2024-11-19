namespace Shop.Core.Dto.OpenWeather;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OpenWeatherInfo
{
    public OpenWeatherLocation Location { get; set; }
    public OpenWeatherCurrent CurrentData { get; set; }
}
