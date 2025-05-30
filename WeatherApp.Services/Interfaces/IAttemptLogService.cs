using WeatherApp.Data.Entities;
using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface IAttemptLogService
    {
        Task<IEnumerable<Data.Entities.AttemptLog>> GetAll();

        Task AddLog(AttemptLogModel log);
    }
}
