using WEATHER.API.Services.Contracts;
using WEATHER.API.Services.Implementation;

namespace WEATHER.API.BackgroundServices
{
    /// <summary>
    /// Background job to fetch weather data from OpenWeather API every minute.
    /// </summary>
    public class ScheduledWeatherFetcherBackgroundService : BackgroundService
    {
        private readonly ILogger<WeatherService> _logger;
        //private readonly IBackgroundWeatherJobService _backgroundWeatherJobService;
        private readonly IServiceProvider _serviceProvider;

        public ScheduledWeatherFetcherBackgroundService(ILogger<WeatherService> logger,
            //IBackgroundWeatherJobService backgroundWeatherJobService,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            //_backgroundWeatherJobService = backgroundWeatherJobService;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            //Schedule
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Weather service started at: {time}", DateTimeOffset.UtcNow);
                using (var scope = _serviceProvider.CreateScope())
                {
                    var backgroundWeatherJobService = scope.ServiceProvider.GetRequiredService<IBackgroundWeatherJobService>();
                    await backgroundWeatherJobService.GetDataAsync();
                }


                // Wait for 1 minute
                // !Could be configurable from settings!
                await Task.Delay(60000, cancellationToken);
            }
        }
    }
}
