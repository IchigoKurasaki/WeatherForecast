using System;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherForecast.Api.Models
{
    public class CityWeather
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("dayOfWeek")]
        public string DayOfWeek { get; set; }

        [BsonElement("day")]
        public string Day { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("maxTemperature")]
        public string MaxTemperature { get; set; }

        [BsonElement("minTemperature")]
        public string MinTemperature { get; set; }

        [BsonElement("rainfall")]
        public string Rainfall { get; set; }

        [BsonElement("windSpeed")]
        public int WindSpeed { get; set; }

        [BsonElement("forecastFor")]
        public DateTime ForecastFor { get; set; }

        [BsonElement("updatedOn")]
        public DateTime UpdatedOn { get; set; }
    }
}
