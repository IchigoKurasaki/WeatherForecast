using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecast.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WeatherForecast.Web.Services
{
    public class ApiService: IApiService
    {
        const string WebApiUrl = @"https://localhost:44312/";
        const string GetWeatherActionUrl = "api/weather/{0}/{1}";
        const string GetCitiesActionUrl = "api/weather/cities";
        const int DaysToForecast = 10;

        HttpClient _client;
        WeatherForecastViewModel _viewModel;

        public ApiService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(WebApiUrl)
            };

            _viewModel = new WeatherForecastViewModel();
        }

        public async Task<WeatherForecastViewModel> GetWeatherByFilter(FilterViewModel filter)
        {
            var response = await _client.GetAsync(string.Format(GetWeatherActionUrl, filter.Date, filter.City));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherForecastData = JsonConvert.DeserializeObject<List<WeatherForecastData>>(content);

                _viewModel.Filter = filter;
                _viewModel.WeatherForecastData = weatherForecastData;
            }

            return _viewModel;
        }

        public async Task<WeatherForecastViewModel> GetCities()
        {
            var response = await _client.GetAsync(GetCitiesActionUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cities = JsonConvert.DeserializeObject<IEnumerable<string>>(content);

                _viewModel.Cities = new SelectList(cities);
            }

            return _viewModel;
        }

        public WeatherForecastViewModel GetDates()
        {
            var dates = new List<string>();

            for (int i = 0; i < DaysToForecast; i++)
            {
                dates.Add(DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
            }

            _viewModel.Dates = new SelectList(dates);

            return _viewModel;
        }
    }
}
