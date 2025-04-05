using AutoMapper;
using WEATHER.API.Data.Models;
using WEATHER.API.Dto;

namespace WEATHER.API.Configuration
{
    public class WeatherMapperProfile : Profile
    {
        public WeatherMapperProfile()
        {
            CreateMap<Weather, WeatherDto>().ReverseMap();
            // Add other mappings
        }
    }
}
