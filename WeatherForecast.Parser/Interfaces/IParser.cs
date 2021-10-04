using System.Collections.Generic;
using WeatherForecast.Parser.Models;

namespace WeatherForecast.Parser.Interfaces
{
    public interface IParser
    {
        public List<WeatherForecastData> ParseAll();
    }
}
