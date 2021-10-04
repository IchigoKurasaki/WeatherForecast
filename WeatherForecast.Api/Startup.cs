using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WeatherForecast.Api.Models;
using WeatherForecast.Api.Repositories;
using WeatherForecast.Api.Services;

namespace WeatherForecast.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICityWeatherRepository, CityWeatherRepository>();

            services.AddScoped<IWeatherService, WeatherService>();

            services.Configure<WeatherDatabaseSettings>(
                Configuration.GetSection(nameof(WeatherDatabaseSettings)));

            services.AddSingleton<WeatherDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<WeatherDatabaseSettings>>().Value);

            services.AddControllers()
                .AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }

        private IMongoDatabase GetDatabase(WeatherDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            return client.GetDatabase(databaseSettings.DatabaseName);
        }
    }
}
