namespace WeatherApp.Models
{
    public class AttemptLogModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string RequestUri { get; set; }

        public int StatusCode { get; set; }

        public string? ErrorMessage { get; set; }
    }
}