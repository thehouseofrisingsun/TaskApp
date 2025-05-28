using Microsoft.EntityFrameworkCore;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories.Interfaces;

namespace WeatherApp.Data.Repositories
{
    public class LocationRepository: ILocationRepository
    {
        private readonly WeatherContext _context;

        public LocationRepository(WeatherContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            return await _context.Locations.ToListAsync();
        }
    }
}
