using Microsoft.EntityFrameworkCore;
using WEATHER.API.Data;
using WEATHER.API.Data.Models;
using WEATHER.API.Services.Contracts;

namespace WEATHER.API.Services.Implementation
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherDbContext _context;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(WeatherDbContext context, ILogger<WeatherService> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Could be paginated and filtered
        public async Task<IEnumerable<Weather>> GetAsync()
        {
            //todo:more complex query
            var data = await _context.Weather.AsNoTracking().ToListAsync();
            
            return data;
        }

        //should be separate methods but for now seems fine approach
        public async Task<Weather> CreateOrUpdateAsync(Weather weather)
        {
            //should be more optimal with invariantCulture etc
            var existingWeather = await _context.Weather
                .FirstOrDefaultAsync(w => w.City == weather.City && w.Country == weather.Country);

            if (existingWeather != null)
            {
                existingWeather.MinTemperature = weather.MinTemperature;
                existingWeather.MaxTemperature = weather.MaxTemperature;
                existingWeather.Modified = DateTime.UtcNow;

                _context.Weather.Update(existingWeather);
            }
            else
            {
                weather.Modified = DateTime.UtcNow;
                await _context.Weather.AddAsync(weather);
            }

            await _context.SaveChangesAsync();
            return existingWeather ?? weather;
        }

    }
}
