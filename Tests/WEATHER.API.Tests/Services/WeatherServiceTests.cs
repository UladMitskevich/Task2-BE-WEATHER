
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using WEATHER.API.Data;
using WEATHER.API.Data.Models;
using WEATHER.API.Services.Implementation;
using WEATHER.API.Tests.Utilities;

namespace WEATHER.API.Tests.Services
{
    /// <summary>
    /// Sample unit tests for WeatherService. Could be improved with more complex scenarios, edge cases, style etc.
    /// </summary>
    public class WeatherServiceTests
    {
        private readonly Mock<WeatherDbContext> _mockContext;
        private readonly Mock<DbSet<Weather>> _mockWeatherDbSet;
        private readonly Mock<ILogger<WeatherService>> _mockLogger;
        private readonly WeatherService _weatherService;

        public WeatherServiceTests()
        {
            _mockContext = new Mock<WeatherDbContext>(new DbContextOptions<WeatherDbContext>());

            _mockWeatherDbSet = new Mock<DbSet<Weather>>();
            _mockLogger = new Mock<ILogger<WeatherService>>();

            // Setup some sample weather data
            var weatherData = new List<Weather>
            {
                new Weather { Id = 1, City = "London", Country = "UK", MinTemperature = 10, MaxTemperature = 15, Modified = DateTime.UtcNow },
                new Weather { Id = 2, City = "New York", Country = "USA", MinTemperature = 20, MaxTemperature = 30, Modified = DateTime.UtcNow }
            }.AsQueryable();

            var asyncQueryProvider = new TestAsyncQueryProvider<Weather>(weatherData.Provider);

            _mockWeatherDbSet.As<IQueryable<Weather>>().Setup(m => m.Provider).Returns(asyncQueryProvider);
            _mockWeatherDbSet.As<IQueryable<Weather>>().Setup(m => m.Expression).Returns(weatherData.Expression);
            _mockWeatherDbSet.As<IQueryable<Weather>>().Setup(m => m.ElementType).Returns(weatherData.ElementType);
            _mockWeatherDbSet.As<IQueryable<Weather>>().Setup(m => m.GetEnumerator()).Returns(weatherData.GetEnumerator());
            _mockWeatherDbSet.As<IAsyncEnumerable<Weather>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>())).Returns(new TestAsyncEnumerator<Weather>(weatherData.GetEnumerator()));

            _mockContext.Setup(c => c.Weather).Returns(_mockWeatherDbSet.Object);

            _weatherService = new WeatherService(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnAllWeatherData()
        {
            // Act
            var result = await _weatherService.GetAsync();

            // Assert
            var weatherList = result.ToList();
            Assert.Equal(2, weatherList.Count);
            Assert.Equal("London", weatherList[0].City);
            Assert.Equal("New York", weatherList[1].City);
        }

        [Fact]
        public async Task CreateOrUpdateAsync_ShouldCreateNewWeatherEntry_WhenWeatherDoesNotExist()
        {
            // Arrange
            var newWeather = new Weather
            {
                City = "Paris",
                Country = "France",
                MinTemperature = 5,
                MaxTemperature = 10
            };

            // Act
            var result = await _weatherService.CreateOrUpdateAsync(newWeather);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Paris", result.City);
            _mockContext.Verify(m => m.Weather.AddAsync(It.IsAny<Weather>(), default), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task CreateOrUpdateAsync_ShouldUpdateExistingWeather_WhenWeatherAlreadyExists()
        {
            // Arrange
            var existingWeather = new Weather
            {
                Id = 1,
                City = "London",
                Country = "UK",
                MinTemperature = 12,
                MaxTemperature = 17
            };

            // Act
            var result = await _weatherService.CreateOrUpdateAsync(existingWeather);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingWeather.MinTemperature, result.MinTemperature);
            Assert.Equal(existingWeather.MaxTemperature, result.MaxTemperature);
            Assert.Equal(existingWeather.City, result.City);
            Assert.Equal(existingWeather.Country, result.Country);

            //_mockContext.Verify(m => m.Weather.Update(It.IsAny<Weather>()), Times.Once);
            //_mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}
