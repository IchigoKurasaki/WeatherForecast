using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WeatherForecast.Parser.Interfaces;
using WeatherForecast.Parser.Models;
using static WeatherForecast.Parser.Services.ParserConsts;

namespace WeatherForecast.Parser.Service
{
    public class WeatherParser : IParser
    {
        public List<WeatherForecastData> ParseAll()
        {
            var weatherForecastDataCollection = new List<WeatherForecastData>();

            var htmlMainDoc = GetHtmlDocument(WeatherUrl);
           
            var mainSection = htmlMainDoc.DocumentNode.QuerySelector(MainSectionSelector);

            var cities = mainSection.QuerySelector(PopularCitiesSelector)
                .ChildNodes.Where(c => c.Name == LinkSelector)
                .Select(q => new
                {
                    Name = q.Attributes[DataNameAttribute].Value,
                    Link = q.Attributes[DataLinkAttribute].Value
                }).ToList();

            foreach (var city in cities)
            {
                var forecastHtmlPage = string.Format(WeatherDetailsUrl, city.Link, ForecastForDays);
                var htmlDoc = GetHtmlDocument(forecastHtmlPage);

                var forecastWidgetNode = htmlDoc.DocumentNode.QuerySelector(ForecastWidgetSelector);

                AddDataToForecastCollection(forecastWidgetNode, city.Name, weatherForecastDataCollection); 
            }

            return weatherForecastDataCollection;
        }

        private HtmlDocument GetHtmlDocument(string url)
        {
            HtmlWeb mainWeb = new HtmlWeb();
            return mainWeb.Load(url);
        }

        private void AddDataToForecastCollection(HtmlNode forecastWidgetNode, string cityName,
                        List<WeatherForecastData> weatherForecastDataCollection)
        {
            var daysOfWeek = forecastWidgetNode.QuerySelectorAll(DaysOfWeekSelector)
                    .Select(q => q.InnerText).ToArray();

            var dates = forecastWidgetNode.QuerySelectorAll(DatesSelector)
                .Select(q => q.NextSibling)
                .Select(q => ConvertToNumber(q.InnerText.Trim()))
                .ToArray();

            var descriptions = forecastWidgetNode.QuerySelectorAll(WeatherDescriptionSelector)
                .Select(q => q.Attributes[DataTextAttribute].Value)
                .ToArray();

            var maxT = forecastWidgetNode.QuerySelectorAll(MaxTemperatureSelector)
                .Select(q => q.SelectSingleNode(SpanSelector))
                .Select(q => q.InnerText)
                .ToArray();

            var minT = forecastWidgetNode.QuerySelectorAll(MinTemperatureSelector)
                .Select(q => q.SelectSingleNode(SpanSelector))
                .Select(q => q.InnerText)
                .ToArray();

            var rainfall = forecastWidgetNode.QuerySelectorAll(RainfallMmSelector)
                .Select(q => string.IsNullOrEmpty(q.InnerText) ? "0" : q.InnerText.Trim())
                .ToArray();

            var windSpeed = forecastWidgetNode.QuerySelectorAll(WindSpeedMsSelector)
                .Select(q => q.InnerText.Trim())
                .ToArray();

            for (var i = 0; i < ForecastForDays; i++)
            {
                weatherForecastDataCollection.Add(new WeatherForecastData()
                {
                    City = cityName,
                    DayOfWeek = daysOfWeek[i],
                    Day = dates[i],
                    Description = descriptions[i],
                    MaxTemperature = maxT[i],
                    MinTemperature = minT[i],
                    Rainfall = rainfall[i],
                    WindSpeed = Convert.ToInt32(windSpeed[i]),
                    ForecastFor = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(dates[i])),
                    UpdatedOn = DateTime.Now
                });
            }
        }

        private string ConvertToNumber(string input)
        {
            return Regex.Replace(input, @"[^\d]", "");
        }
    }
}
