using WeatherApp.Data.Entities;

namespace WeatherApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(WeatherContext context)
        {
            context.Database.EnsureCreated();

            if (context.Locations.Any())
            {
                return;
            }

            var locations = new Location[]
            {
                new Location{City = "Riga", Country ="Latvia", Latitude = 56.9475, Longitude =24.1069 },
                new Location{City = "Rome", Country ="Italy", Latitude = 41.9028, Longitude =12.4964 },
                new Location{City = "London", Country ="United Kingdom", Latitude = 51.5072, Longitude = -0.1276 }
            };
            foreach (Location loc in locations)
            {
                context.Locations.Add(loc);
            }
            context.SaveChanges();
        }
    }
}
