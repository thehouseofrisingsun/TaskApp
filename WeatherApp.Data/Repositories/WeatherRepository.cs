using Microsoft.EntityFrameworkCore;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories.Interfaces;

namespace WeatherApp.Data.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherContext _weatherContext;

        public WeatherRepository(WeatherContext weatherContext)
        {
            _weatherContext = weatherContext;
        }

        public Task<IEnumerable<CurrentWeather>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task BulkUpdateCurrentWeather(IEnumerable<CurrentWeather> weatherList)
        {
            _weatherContext.CurrentWeather.AddRange(weatherList);
            await _weatherContext.SaveChangesAsync();
        }

        public async Task<List<CurrentWeather>> GetMinTemperature()
        {
            var data = await _weatherContext.CurrentWeather
                .FromSqlRaw("SELECT " +
                            "cw.City, " +
                            "cw.Country, " +
                            "cw.Temperature, " +
                            "max(cw.LastUpdate) as LastUpdate, " +
                            "max(ID) as ID , " +
                            "max(Latitude) as Latitude," +
                            "max(Longitude) as Longitude, " +
                            "max(windSpeed) as WindSpeed " +
                            "FROM [WeatherAnalytics].[dbo].[CurrentWeather] AS cw " +
                            "LEFT JOIN (" +
                                "SELECT " +
                                    "min(Temperature) AS min_temperature, " +
                                   "City, " +
                                    "Country " +
                                "FROM [WeatherAnalytics].[dbo].[CurrentWeather] " +
                                "GROUP BY City, Country " +
                            ") subquery ON subquery.City = cw.City and subquery.Country = cw.Country " +
                            "WHERE subquery.min_temperature = cw.temperature " +
                            "GROUP BY cw.City, cw.Country, temperature " +
                            "ORDER BY City, Country ASC").ToListAsync();

            return data;
        }

        public async Task<List<CurrentWeather>> GetMaxWindSpeed()
        {
            var data = await _weatherContext.CurrentWeather
                .FromSqlRaw("SELECT " +
                            "cw.City, " +
                            "cw.Country, " +
                            "cw.WindSpeed, " +
                            "max(cw.LastUpdate) as LastUpdate, " +
                            "max(ID) as ID, " +
                            "max(Latitude) as Latitude, " +
                            "max(Longitude) as Longitude, " +
                            "max(Temperature) as Temperature " +
                            "FROM [WeatherAnalytics].[dbo].[CurrentWeather] AS cw " +
                            "LEFT JOIN (" +
                                "SELECT " +
                                    "max(WindSpeed) AS max_windspeed, " +
                                    "City, " +
                                    "Country " +
                                "FROM [WeatherAnalytics].[dbo].[CurrentWeather] " +
                                "GROUP BY City, Country " +
                            ") subquery ON subquery.City = cw.City and subquery.Country = cw.Country " +
                            "WHERE subquery.max_windspeed = cw.WindSpeed " +
                            "GROUP BY cw.City, cw.Country, WindSpeed " +
                            "ORDER BY City, Country ASC ").ToListAsync();

            return data;
        }
    }
}
