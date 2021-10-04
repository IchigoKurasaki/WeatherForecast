using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using WeatherForecast.Web.Models;
using WeatherForecast.Web.Services;

namespace WeatherForecast.Web.Controllers
{
    public class HomeController : Controller
    {
        IApiService _apiService;

        public HomeController(IApiService apiService)
        {
            _apiService = apiService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _apiService.GetCities();
            model = _apiService.GetDates();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(FilterViewModel filter)
        {
            var model = await _apiService.GetWeatherByFilter(filter);
            model = await _apiService.GetCities();
            model = _apiService.GetDates();

            return View(model);
        }
    }
}

