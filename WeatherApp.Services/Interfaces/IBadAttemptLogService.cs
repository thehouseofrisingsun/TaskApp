using WeatherApp.Data.Entities;
using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface IBadAttemptLogService
    {
        Task<IEnumerable<BadAttemptLog>> GetAll();

        Task AddLog(BadAttemptLogModel log);
    }
}
