namespace Shop.Core.Dto.Weather;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LocationWeatherResultDto
{
    public string CityName { get; set; }

    public List<CurrentConditionDto> CurrentConditions { get; set; } = [];
}
