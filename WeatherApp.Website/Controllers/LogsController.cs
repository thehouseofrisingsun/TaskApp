using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Diagnostics;
using WeatherApp.Services.Interfaces;
using WeatherApp.Website.Models;

namespace WeatherApp.WebSite.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        private readonly IAttemptLogService _logService;

        public LogsController(IAttemptLogService logService)
        {
            _logService = logService;
        }

        [Route("logs")]
        public async Task<JsonResult> GetLogs()
        {
            var logs = await _logService.GetAll();

            return Json(logs);
        }
    }
}
