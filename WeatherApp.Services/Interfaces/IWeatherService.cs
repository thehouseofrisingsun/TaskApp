using WeatherApp.Data.Entities;
using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<CurrentWeatherModel> GetCurrentWeather(LocationModel location);

        Task BulkUpdateCurrentWeatherInfo(List<CurrentWeatherModel> currentWeather);

        Task<List<MinTemperatureModel>> GetMinTemperature();

        Task<IList<MaxWindSpeedModel>> GetMaxWindSpeed();
    }
}
