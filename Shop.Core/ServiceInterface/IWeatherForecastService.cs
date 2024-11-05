namespace Shop.Core.ServiceInterface;

using Shop.Core.Dto.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IWeatherForecastService
{
    public Task<LocationWeatherResultDto> GetWeather(string cityName);
}
