using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApp.Services.Interfaces;
using WeatherApp.Website.Models;

namespace WeatherApp.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherService _weatherService;
        private readonly ILocationService _locationService;


        public HomeController(ILogger<HomeController> logger, IWeatherService weatherService, ILocationService locationService)
        {
            _logger = logger;
            _weatherService = weatherService;
            _locationService = locationService;
        }


        public async Task<IActionResult> MinTemperature()
        {
            var data = await _weatherService.GetMinTemperature();
            return Json(data);
        }

        public async Task<IActionResult> MaxWindSpeed()
        {
            var data = await _weatherService.GetMaxWindSpeed();
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
