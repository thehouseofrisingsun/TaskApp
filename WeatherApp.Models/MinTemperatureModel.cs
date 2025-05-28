namespace WeatherApp.Models
{
    public class MinTemperatureModel
    {
        public double MinTemperature { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime LastUpdate { get; set; }
    }
}
