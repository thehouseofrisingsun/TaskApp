using WeatherApp.Data.Entities;

namespace WeatherApp.Data.Repositories.Interfaces
{
    public interface IBadAttemptLogRepository
    {
        Task<IEnumerable<BadAttemptLog>> GetAll();
        Task  AddLog(BadAttemptLog log);
    }
}
