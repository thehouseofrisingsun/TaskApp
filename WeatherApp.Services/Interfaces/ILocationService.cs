using WeatherApp.Data.Entities;

namespace WeatherApp.Services.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetLocations();
    }
}
