using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WeatherForecast.Web.Models
{
    public class WeatherForecastViewModel
    {
        public SelectList Cities { get; set; }

        public SelectList Dates { get; set; } 

        public FilterViewModel Filter { get; set; }

        public IEnumerable<WeatherForecastData> WeatherForecastData { get; set; }
    }
}
