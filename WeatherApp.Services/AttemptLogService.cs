using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories.Interfaces;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.Services
{
    public class AttemptLogService : IAttemptLogService
    {
        private readonly IAttemptLogRepository _attemptLogRepository;

        public AttemptLogService(IAttemptLogRepository attemptLogRepository)
        {
            _attemptLogRepository = attemptLogRepository;
        }

        public async Task AddLog(AttemptLogModel log)
        {
            var logEntity = new Data.Entities.AttemptLog
            {
                Date = log.Date,
                ErrorMessage = log.ErrorMessage,
                RequestUri = log.RequestUri,
                StatusCode = log.StatusCode
            };

           await _attemptLogRepository.AddLog(logEntity);
        }

        public Task<IEnumerable<Data.Entities.AttemptLog>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
