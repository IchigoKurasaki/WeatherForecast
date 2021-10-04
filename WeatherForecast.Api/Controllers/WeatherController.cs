using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WeatherForecast.Api.Models;
using WeatherForecast.Api.Services;

namespace WeatherForecast.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        
        [HttpGet]
        [Route("cities")]
        public IEnumerable<string> GetCities()
        {
            return _weatherService.GetCities();
        }
        

        [HttpGet]
        [Route("{date}/{city}")]
        public IEnumerable<CityWeather> GetForecastByDate(string date, string city)
        {
            return _weatherService.GetWeather(date, city);
        }

        [HttpPost]
        public async Task<IEnumerable<CityWeather>> CreateForecast([FromBody]List<CityWeather> weather)
        {
            await _weatherService.CreateManyAsync(weather);

            return weather;
        }
    }
}
