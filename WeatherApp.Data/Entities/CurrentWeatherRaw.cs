namespace WeatherApp.Data.Entities
{
    public class CurrentWeatherRaw
    {
        public string Data { get; set; } = string.Empty;
        public DateTime LastUpdate { get; set; }
    }
}
