namespace WEATHER.API.Services.Contracts
{
    public interface IBackgroundWeatherJobService
    {
        Task GetDataAsync();
    }
}
