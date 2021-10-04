using System.Threading.Tasks;
using WeatherForecast.Web.Models;

namespace WeatherForecast.Web.Services
{
    public interface IApiService
    {
        public Task<WeatherForecastViewModel> GetWeatherByFilter(FilterViewModel filter);

        public Task<WeatherForecastViewModel> GetCities();

        public WeatherForecastViewModel GetDates();
    }
}
