namespace Shop.Core.ServiceInterface;

using Shop.Core.Dto.OpenWeather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IOpenWeatherService
{
    public Task<List<OpenWeatherInfo>> GetWeather(string cityName);
}
