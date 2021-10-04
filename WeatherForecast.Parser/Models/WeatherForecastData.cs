using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherForecast.Parser.Models
{
    public class WeatherForecastData
    {
        public string City { get; set; }

        public string DayOfWeek { get; set; }

        public string Day { get; set; }

        public string Description { get; set; }

        public string MaxTemperature { get; set; }

        public string MinTemperature { get; set; }

        public string Rainfall { get; set; }

        public int WindSpeed { get; set; }

        public DateTime ForecastFor { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
