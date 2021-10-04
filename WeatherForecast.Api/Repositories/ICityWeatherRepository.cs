using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WeatherForecast.Api.Models;

namespace WeatherForecast.Api.Repositories
{
    public interface ICityWeatherRepository
    {
        public IQueryable<CityWeather> AsQuerable();

        public CityWeather GetById(string id);

        public Task<CityWeather> GetByIdAsync(string id);

        public void Create(CityWeather cityWeather);

        public Task CreateAsync(CityWeather cityWeather);

        public void CreateMany(IEnumerable<CityWeather> citiesWeather);

        public Task CreateManyAsync(IEnumerable<CityWeather> citiesWeather);

        public void Delete(string id);

        public Task DeleteAsync(string id);

        public IEnumerable<CityWeather> Find(FilterDefinition<CityWeather> filter);

        public IEnumerable<string> GetCities();
    }
}
