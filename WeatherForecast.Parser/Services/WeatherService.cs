using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.Parser.Models;

namespace WeatherForecast.Parser.Services
{
    public class WeatherService
    {
        const string WebApiUrl = @"https://localhost:44312";
        const string CreateForecastActionUrl = "api/weather";

        public async Task CreateForecastData(List<WeatherForecastData> weather)
        {
            var client = new Client(WebApiUrl, CreateForecastActionUrl);
            await client.CreateForecastBatch(weather);
        }
    }
}
