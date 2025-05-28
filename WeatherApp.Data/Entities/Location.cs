namespace WeatherApp.Data.Entities
{
    public class Location
    {
        public int ID { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public double Latitude { get; set; }    
        public double Longitude { get; set; }
    }
}
