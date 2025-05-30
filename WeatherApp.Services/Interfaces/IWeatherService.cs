using WeatherApp.Data.Entities;
using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<CurrentWeatherModel> GetCurrentWeather(LocationModel location);

        Task BulkUpdateCurrentWeatherInfo(List<CurrentWeatherModel> currentWeather);

        Task<List<MinMaxTemperatureModel>> GetMinMaxTemperature(DateTime from, DateTime to);
    }
}
