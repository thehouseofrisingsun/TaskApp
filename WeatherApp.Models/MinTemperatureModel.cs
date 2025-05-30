namespace WeatherApp.Models
{
    public class MinMaxTemperatureModel
    {
        public double MinTemperature { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public double MaxTemperature { get; set; }
        public DateTime MinTemperatureLastUpdate { get; set; }
        public DateTime MaxTemperatureLastUpdate { get; set; }

    }
}
