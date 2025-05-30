using Microsoft.EntityFrameworkCore;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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


        public async Task<List<WeatherStatInfo>> GetMinMaxTemperature(DateTime from, DateTime to)
        {
            var result = new List<WeatherStatInfo>();

            using var connection = _weatherContext.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT 
                main.Country, 
                main.City, 
                main.max_temperature as MaxTemperature, 
                main.min_temperature as MinTemperature, 
                MAX(max_t_subq.LastUpdate) AS MaxTemperatureLastUpdate,
                MAX(min_t_subq.LastUpdate) AS MinTemperatureLastUpdate
                FROM (
                SELECT
                    Country,
                    City,
                    MAX(Temperature) AS max_temperature,
                    MIN(Temperature) AS min_temperature
                FROM [WeatherAnalytics].[dbo].[CurrentWeather]
                WHERE LastUpdate >= @from
                    AND LastUpdate <= @to
                GROUP BY Country, City
                ) main
                LEFT JOIN (
                SELECT
                    Country,
                    City,
                    Temperature,
                    LastUpdate
                FROM [WeatherAnalytics].[dbo].[CurrentWeather]
                ) AS max_t_subq ON max_t_subq.Country = main.Country 
                            AND max_t_subq.City = main.City
                            AND max_t_subq.Temperature = main.max_temperature
                LEFT JOIN (
                SELECT
                    Country,
                    City,
                    Temperature,
                    LastUpdate
                FROM [WeatherAnalytics].[dbo].[CurrentWeather]
                ) AS min_t_subq ON min_t_subq.Country = main.Country 
                            AND min_t_subq.City = main.City
                            AND min_t_subq.Temperature = main.min_temperature
                GROUP BY main.Country, main.City, main.max_temperature, main.min_temperature";

            var fromParam = command.CreateParameter();
            fromParam.ParameterName = "@from";
            fromParam.Value = from;
            command.Parameters.Add(fromParam);

            var toParam = command.CreateParameter();
            toParam.ParameterName = "@to";
            toParam.Value = to;
            command.Parameters.Add(toParam);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new WeatherStatInfo
                {
                    Country = reader.GetString(0),
                    City = reader.GetString(1),
                    MaxTemperature = reader.GetDouble(2),
                    MinTemperature = reader.GetDouble(3),
                    MaxTemperatureLastUpdate = reader.GetDateTime(4),
                    MinTemperatureLastUpdate = reader.GetDateTime(5)
                });
            }

            return result;
        }
      
    }
}
