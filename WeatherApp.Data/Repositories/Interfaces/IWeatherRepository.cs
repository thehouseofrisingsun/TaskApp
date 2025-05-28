using WeatherApp.Data.Entities;

namespace WeatherApp.Data.Repositories.Interfaces
{
    public interface IWeatherRepository
    {
        Task<IEnumerable<CurrentWeather>> GetAll();
        Task BulkUpdateCurrentWeather(IEnumerable<CurrentWeather> weatherList);

        Task<List<CurrentWeather>> GetMinTemperature();

        Task<List<CurrentWeather>> GetMaxWindSpeed();
    }
}
