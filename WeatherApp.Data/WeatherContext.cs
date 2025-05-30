using Microsoft.EntityFrameworkCore;
using WeatherApp.Data.Entities;

namespace WeatherApp.Data
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {
        }

        public DbSet<CurrentWeather> CurrentWeather { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<AttemptLog> AttemptLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentWeather>().ToTable("CurrentWeather");
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<AttemptLog>().ToTable("AttemptLog");
        }
    }
}
