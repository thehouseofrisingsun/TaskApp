using Microsoft.EntityFrameworkCore;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories.Interfaces;

namespace WeatherApp.Data.Repositories
{
    public class BadAttemptLogRepository : IBadAttemptLogRepository
    {
        private readonly WeatherContext _context;

        public BadAttemptLogRepository(WeatherContext context)
        {
            _context = context;
        }

        public async Task AddLog(BadAttemptLog log) => await _context.BadAttemptLogs.AddAsync(log);

        public async Task<IEnumerable<BadAttemptLog>> GetAll()
        {
            return await _context.BadAttemptLogs.ToListAsync();
        }
    }
}
