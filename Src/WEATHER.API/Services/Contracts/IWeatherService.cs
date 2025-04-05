using WEATHER.API.Data.Models;

namespace WEATHER.API.Services.Contracts
{
    public interface IWeatherService
    {
        Task<IEnumerable<Weather>> GetAsync();
        //Task<Weather> CreateAsync(Weather weatherDto);
        //Task<bool> UpdateAsync(int id, Weather weatherDto);
        Task<Weather> CreateOrUpdateAsync(Weather weather);
    }
}
