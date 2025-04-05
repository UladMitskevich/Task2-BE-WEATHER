using System.Text.Json;
using WEATHER.API.Data;
using WEATHER.API.Data.Models;
using WEATHER.API.Services.Contracts;

namespace WEATHER.API.Services.Implementation
{
    public class BackgroundWeatherJobService : IBackgroundWeatherJobService
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        //constants SHOULD be used from settings with OPtions or monitor or etc.
        private readonly string apiKey;
        private readonly string[] cities = { "London,uk", "Birmingham,uk", "Leeds,uk", "Paris,fr",
            "Marseille,fr" };

        public BackgroundWeatherJobService(ILogger<WeatherService> logger,
            IWeatherService weatherService, IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _weatherService = weatherService;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            //should be checks added
            //should be separate models for each config string in settings
            apiKey = configuration["OpenWeatherAPIKey"];
        }

        public async Task GetDataAsync()
        {
            var client = _httpClientFactory.CreateClient();

            //just simple loop to get all cities
            foreach (var city in cities)
            {
                try
                {
                    //additinal validations should be added
                    //but for now intentionally skipped
                    //should be placed in separate method/service for json manipulations
                    //could be done in differeent ways
                    //Should be mmodel defined for that payload and parse in it not in JsonElement
                    var response = await client.GetStringAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={apiKey}");
                    var weather = JsonSerializer.Deserialize<JsonElement>(response);
                    var cityName = weather.GetProperty("name").GetString();
                    var tempMin = weather.GetProperty("main").GetProperty("temp_min").GetDecimal();
                    var tempMax = weather.GetProperty("main").GetProperty("temp_max").GetDecimal();

                    _logger.LogInformation($"City: {cityName}, Min Temp: {tempMin}, Max Temp: {tempMax}");

                    //additionla checks skipped for now
                    var weatherData = new Weather
                    {
                        Country = city.Split(',')[1],
                        City = cityName,
                        MinTemperature = tempMin,
                        MaxTemperature = tempMax,
                        Modified = DateTime.UtcNow
                    };

                    await _weatherService.CreateOrUpdateAsync(weatherData);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to fetch weather data for {city}: {ex.Message}");
                }
            }

        }

    }
}
