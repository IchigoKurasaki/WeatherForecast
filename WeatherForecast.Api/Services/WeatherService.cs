using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

using WeatherForecast.Api.Models;
using WeatherForecast.Api.Repositories;

namespace WeatherForecast.Api.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ICityWeatherRepository _cityWeatherRepository;

        public WeatherService(ICityWeatherRepository cityWeatherRepository)
        {
            _cityWeatherRepository = cityWeatherRepository;
        }

        public IQueryable<CityWeather> GetAll()
        {
            return _cityWeatherRepository.AsQuerable();
        }

        public CityWeather GetById(string id)
        {
            return _cityWeatherRepository.GetById(id);
        }

        public async Task<CityWeather> GetByIdAsync(string id)
        {
            return await _cityWeatherRepository.GetByIdAsync(id);
        }

        public void Create(CityWeather cityWeather)
        {
            _cityWeatherRepository.Create(cityWeather);
        }

        public async Task CreateAsync(CityWeather cityWeather)
        {
            await _cityWeatherRepository.CreateAsync(cityWeather);
        }

        public void CreateMany(ICollection<CityWeather> citiesWeather)
        {
            _cityWeatherRepository.CreateMany(citiesWeather);
        }

        public async Task CreateManyAsync(ICollection<CityWeather> citiesWeather)
        {
            await _cityWeatherRepository.CreateManyAsync(citiesWeather);
        }

        public void Delete(string id)
        {
            _cityWeatherRepository.Delete(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _cityWeatherRepository.DeleteAsync(id);
        }

        public IEnumerable<CityWeather> GetWeather(string forecastDate, string city)
        {
            var builder = new FilterDefinitionBuilder<CityWeather>();
            var filter = builder.Empty;

            if (!String.IsNullOrWhiteSpace(city))
            {
                filter = filter & builder.Regex("City", new BsonRegularExpression(city));
            }
            if (!String.IsNullOrWhiteSpace(forecastDate))
            {
                var forecastDateTime = Convert.ToDateTime(forecastDate);
                filter = filter & builder.Where(b => b.ForecastFor > forecastDateTime.AddDays(-1) 
                                                  && b.ForecastFor < forecastDateTime.AddDays(1));
            }

            return _cityWeatherRepository.Find(filter).OrderByDescending(x => x.UpdatedOn).Take(1);
        }

        public IEnumerable<string> GetCities()
        {
            return _cityWeatherRepository.GetCities();
        }
    }
}
