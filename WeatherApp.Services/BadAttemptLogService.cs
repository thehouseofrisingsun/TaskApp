using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories.Interfaces;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class BadAttemptLogService : IBadAttemptLogService
    {
        private readonly IBadAttemptLogRepository _badAttemptLogRepository;

        public BadAttemptLogService(IBadAttemptLogRepository badAttemptLogRepository)
        {
            _badAttemptLogRepository = badAttemptLogRepository;
        }

        public async Task AddLog(BadAttemptLogModel log)
        {
            var logEntity = new BadAttemptLog
            {
                Date = log.Date,
                ErrorMessage = log.ErrorMessage
            };

           await _badAttemptLogRepository.AddLog(logEntity);
        }

        public Task<IEnumerable<BadAttemptLog>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
