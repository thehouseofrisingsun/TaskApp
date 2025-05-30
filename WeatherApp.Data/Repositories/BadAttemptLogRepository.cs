using Microsoft.EntityFrameworkCore;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories.Interfaces;

namespace WeatherApp.Data.Repositories
{
    public class AttemptLogRepository : IAttemptLogRepository
    {
        private readonly WeatherContext _context;

        public AttemptLogRepository(WeatherContext context)
        {
            _context = context;
        }

        public async Task AddLog(AttemptLog log) => await _context.AttemptLogs.AddAsync(log);

        public async Task<IEnumerable<AttemptLog>> GetAll()
        {
            return await _context.AttemptLogs.ToListAsync();
        }
    }
}
