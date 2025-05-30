using WeatherApp.Data.Entities;

namespace WeatherApp.Data.Repositories.Interfaces
{
    public interface IAttemptLogRepository
    {
        Task<IEnumerable<AttemptLog>> GetAll();
        Task  AddLog(AttemptLog log);
    }
}
