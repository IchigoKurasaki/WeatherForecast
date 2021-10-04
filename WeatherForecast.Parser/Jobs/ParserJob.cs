using Quartz;
using System.Threading.Tasks;
using WeatherForecast.Parser.Service;
using WeatherForecast.Parser.Services;

namespace WeatherForecast.Parser
{
    public class ParserJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var parser = new WeatherParser();
            var weatherData = parser.ParseAll();

            WeatherService service = new WeatherService();
            await service.CreateForecastData(weatherData);
        }
    }
}
