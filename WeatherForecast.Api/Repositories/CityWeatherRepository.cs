using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

using WeatherForecast.Api.Models;

namespace WeatherForecast.Api.Repositories
{
    public class CityWeatherRepository : ICityWeatherRepository
    {
        private readonly IMongoCollection<CityWeather> _cityWeatherCollection;

        public CityWeatherRepository(WeatherDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _cityWeatherCollection = database.GetCollection<CityWeather>(databaseSettings.WeatherCollectionName);
        }

        public IQueryable<CityWeather> AsQuerable()
        {
            return _cityWeatherCollection.AsQueryable();
        }

        public CityWeather GetById(string id)
        {
            return _cityWeatherCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefault();
        }

        public async Task<CityWeather> GetByIdAsync(string id)
        {
            return await _cityWeatherCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public void Create(CityWeather cityWeather)
        {
            _cityWeatherCollection.InsertOne(cityWeather);
        }

        public async Task CreateAsync(CityWeather cityWeather)
        {
            await _cityWeatherCollection.InsertOneAsync(cityWeather);
        }

        public void CreateMany(IEnumerable<CityWeather> citiesWeather)
        {
            _cityWeatherCollection.InsertMany(citiesWeather);
        }

        public async Task CreateManyAsync(IEnumerable<CityWeather> citiesWeather)
        {
            await _cityWeatherCollection.InsertManyAsync(citiesWeather);
        }

        public void Delete(string id)
        {
            _cityWeatherCollection.DeleteOne(new BsonDocument("_id", new ObjectId(id)));
        }

        public async Task DeleteAsync(string id)
        {
            await _cityWeatherCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        public IEnumerable<CityWeather> Find(FilterDefinition<CityWeather> filter)
        {
            return _cityWeatherCollection.Find(filter).ToList();
        }

        public IEnumerable<string> GetCities()
        {
            var builder = new FilterDefinitionBuilder<CityWeather>();
            var filter = builder.Empty;
            filter = filter & builder.Where(x => x.ForecastFor >= DateTime.Now && x.ForecastFor <= DateTime.Now.AddDays(10));
            return _cityWeatherCollection.Distinct(x => x.City, filter).ToEnumerable();
        }
    }
}
