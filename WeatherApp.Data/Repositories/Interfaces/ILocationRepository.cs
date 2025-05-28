using WeatherApp.Data.Entities;

namespace WeatherApp.Data.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAll();
    }
}
