using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherForecast.Parser
{
    public class Client
    {
        public string Url { get; set; }
        public string ActionUrl { get; set; }

        public Client(string url, string actionUrl)
        {
            Url = url;
            ActionUrl = actionUrl;
        }
        public async Task CreateForecastBatch<T>(List<T> data)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(Url)
            };

            var dataJson = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json");

            var httpResponse = await client.PostAsync(ActionUrl, dataJson);

            httpResponse.EnsureSuccessStatusCode();
        }
    }
}