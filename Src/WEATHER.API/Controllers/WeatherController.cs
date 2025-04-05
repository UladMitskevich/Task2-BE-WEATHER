using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WEATHER.API.Dto;
using WEATHER.API.Services.Contracts;

namespace WEATHER.API.Controllers
{
    //auth schema configs etc.
    //versioning omitted
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly ILogger<WeatherController> _logger;
        private readonly IMapper _mapper;

        public WeatherController(IWeatherService IWeatherService,
            ILogger<WeatherController> logger, IMapper mapper)
        {
            _weatherService = IWeatherService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        //could be search pagination etc.
        public async Task<ActionResult<IEnumerable<WeatherDto>>> Get()
        {
            var data = await _weatherService.GetAsync();
            //pagination
            return Ok(_mapper.Map<IEnumerable<WeatherDto>>(data));
        }
    }
}
