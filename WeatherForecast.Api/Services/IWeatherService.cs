using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.Api.Models;

namespace WeatherForecast.Api.Services
{
    public interface IWeatherService
    {
        public IQueryable<CityWeather> GetAll();

        public CityWeather GetById(string id);

        public Task<CityWeather> GetByIdAsync(string id);

        public void Create(CityWeather cityWeather);

        public Task CreateAsync(CityWeather cityWeather);

        public void CreateMany(ICollection<CityWeather> citiesWeather);

        public Task CreateManyAsync(ICollection<CityWeather> citiesWeather);

        public void Delete(string id);

        public Task DeleteAsync(string id);

        public IEnumerable<CityWeather> GetWeather(string forecastDate, string city);

        public IEnumerable<string> GetCities();
    }
}
