using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories.Interfaces;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<Location>> GetLocations()
        {
            return await _locationRepository.GetAll();
        }
    }
}
