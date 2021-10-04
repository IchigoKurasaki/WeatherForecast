namespace WeatherForecast.Parser.Services
{
    public static class ParserConsts
    {
        public const int ForecastForDays = 10;
        public const string WeatherUrl = @"https://gismeteo.ru";
        public const string WeatherDetailsUrl = @"https://www.gismeteo.ru{0}{1}-days/";

        public const string MainSectionSelector = "[class='main']";
        public const string PopularCitiesSelector = "[id='noscript']";
        public const string ForecastWidgetSelector = "[data-widget-id='forecast']";
        public const string DaysOfWeekSelector = "[class='w_date__day']";
        public const string DatesSelector = "[class='w_date__day']";
        public const string WeatherDescriptionSelector = "[class='tooltip']";
        public const string MaxTemperatureSelector = "[class='maxt']";
        public const string MinTemperatureSelector = "[class='mint']";
        public const string RainfallMmSelector = "[class='w_prec__value']";
        public const string WindSpeedMsSelector = "[class='unit unit_wind_m_s']";
        public const string SpanSelector = "span";
        public const string LinkSelector = "a";

        public const string DataNameAttribute = "data-name";
        public const string DataTextAttribute = "data-text";
        public const string DataLinkAttribute = "href";

    }
}
