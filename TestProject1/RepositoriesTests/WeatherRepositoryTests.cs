using Microsoft.EntityFrameworkCore;
using WeatherApp.Data;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Repositories;


namespace WeatherApp.Tests.RepositoriesTests
{
    [TestFixture]
    public class WeatherRepositoryTests
    {
        private WeatherContext _context;
        private WeatherRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WeatherContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
            .Options;

            _context = new WeatherContext(options);
            _repository = new WeatherRepository(_context);
        }

        [Test]
        public async Task BulkUpdateCurrentWeather_ShouldShouldUpdateDatabaseWithRecords()
        {
            // Arrange
            var weatherData = new List<CurrentWeather>
        {
            new CurrentWeather { City = "TestCity1", Country = "TestCountry1", Latitude = 44.5, Longitude = 20.4, Temperature = 12, WindSpeed = 9 },
            new CurrentWeather { City = "TestCity2", Country = "TestCountry2", Latitude = 41.5, Longitude = 22.4, Temperature = 15, WindSpeed = 6 },
            new CurrentWeather { City = "TestCity3", Country = "TestCountry3", Latitude = 42.5, Longitude = 23.4, Temperature = 16, WindSpeed = 7 }
        };
            // Act
            await _repository.BulkUpdateCurrentWeather(weatherData);
            var result = await _context.CurrentWeather.FindAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("TestCountry1", result.Country);
        }


        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}