using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Diagnostics;
using WeatherApp.Services.Interfaces;
using WeatherApp.Website.Models;

namespace WeatherApp.WebSite.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;
        private readonly ILocationService _locationService;


        public WeatherController(IWeatherService weatherService, ILocationService locationService)
        {
            _weatherService = weatherService;
            _locationService = locationService;
        }

        [Route("minMaxTemperature")]
        public async Task<JsonResult> MinMaxTemperature([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var toDate = to ?? DateTime.UtcNow;
            var fromDate = from ?? toDate.AddDays(-7);

            var data = await _weatherService.GetMinMaxTemperature(fromDate, toDate);
            return Json(data);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
