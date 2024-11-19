namespace Shop.ApplicationServices.SpaceshipServices;

using Shop.Core.Dto.OpenWeather;
using Shop.Core.ServiceInterface;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class OpenWeatherService : IOpenWeatherService
{
    private const string API_KEY = "f66a1929014f894d86294233e3d362b0";

    public async Task<List<OpenWeatherInfo>> GetWeather(string cityName)
    {
        //
        // Steps:
        //  1. Lookup geo code
        //  2. Iterate over results
        //  3. Lookup current weather data for each result
        //
        string url = $"https://api.openweathermap.org/geo/1.0/direct?q={cityName}&appid={API_KEY}&limit=5";

        using (WebClient c = new()) 
        {
            string json = await c.DownloadStringTaskAsync(url);
            List<OpenWeatherLocation>? locations = JsonSerializer.Deserialize<List<OpenWeatherLocation>>(json);

            if (locations == null || locations.Count == 0) 
            {
                return [];
            }

            List<OpenWeatherInfo> result = [];

            foreach (var item in locations)
            {
                double lat = item.Lat;
                double lon = item.Lon;
                string locationUrl = $"https://api.openweathermap.org/data/2.5/weather?units=metric&lat={lat}&lon={lon}&appid={API_KEY}";

                string currentDataJson = await c.DownloadStringTaskAsync(locationUrl);
                OpenWeatherCurrent? current = JsonSerializer.Deserialize<OpenWeatherCurrent>(currentDataJson);

                if (current == null) 
                {
                    continue;
                }

                // Figure out timezone name
                int tz = current.Timezone;
                string tzName = TimeSpan.FromSeconds(Math.Abs(tz)).ToString(@"hh\:mm");
                if (tz < 0) {
                    current.TimezoneName = "-" + tzName;
                } else {
                    current.TimezoneName = "+" + tzName;
                }

                OpenWeatherInfo info = new() 
                {
                    CurrentData = current,
                    Location = item
                };
                result.Add(info);
            }

            return result;
        }
    }
}
