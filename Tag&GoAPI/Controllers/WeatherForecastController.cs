using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_GoAPI.Hubs;
using Tag_GoAPI.Models;
using Tag_GoAPI.Tools;
using Tag_Go.DAL.Interfaces;
using System.Security.Cryptography;
using Microsoft.AspNetCore.SignalR;
using Tag_Go.DAL.Repositories;
using Tag_GoAPI.DTOs.Forms;

namespace Tag_GoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
#nullable disable
        private static readonly string[] TemperatureC = new[]
        {
            "0°", "10°", "20°", "30°", "40°", "50°", "60°"
        };
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private static readonly string[] Descriptions = new[]
        {
            "Fair", "Normal", "UnNatural", "Catastrophic", "Apocaliptic"
        };
        private static readonly string[] Humidities = new[]
        {
            "00°", "10°", "20°", "30°", "40°", "50°" , "60°", "70°", "80°", "90°", "100°"
        };
        private static readonly string[] Precipitations = new[]
        {
            "0%", "10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%"
        };
        private readonly IWeatherForecastRepository _forecastRepository;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastHub _hub;
        private readonly Dictionary<string, WeatherForecastHub> _currentWeatherForecast = new Dictionary<string, WeatherForecastHub>();

        public WeatherForecastController(IWeatherForecastRepository forecastRepository, ILogger<WeatherForecastController> logger, WeatherForecastHub hub)
        {
            _forecastRepository = forecastRepository;
            _logger = logger;
            _hub = hub;
        }
        //public async Task<IEnumerable<WeatherForecast>> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(Index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(Index),
        //        TemperatureC = TemperatureC[Random.Shared.Next(-20, 55)],
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)],
        //        Description = Descriptions[Random.Shared.Next(Descriptions.Length)],
        //        Humidity = Humidities[Random.Shared.Next(-1, 100)],
        //        Precipitation = Precipitations[Random.Shared.Next(0, 1000)]
        //    })
        //    .ToArray();
        //}
        [HttpGet]
        public async Task<IActionResult> GetAllWeatherForecasts()
        {
            try
            {
                var activeWeatherForecasts = await _forecastRepository.GetAllWeatherForecasts();
                return Ok(activeWeatherForecasts);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        //public IActionResult GetAll()
        //{
        //    return Ok(_forecastRepository.GetAll());
        //}
        [HttpGet("{weatherForecast_Id}")]
        public IActionResult GetByIdWeatherForecast(int weatherForecast_Id)
        {
            try
            {
                var weatherforecast = _forecastRepository.GetByIdWeatherForecast(weatherForecast_Id);
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                return Ok(_forecastRepository.GetByIdWeatherForecast(weatherForecast_Id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }

        }
        //[HttpPost]
        //public async Task<IActionResult> Create(WeatherForecastRegisterForm weatherregisterform)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();
        //    if (_forecastRepository.Create(weatherregisterform.WeatherForecastToDal()))
        //    {
        //        await _hub.RefreshWeatherForecast();
        //        return Ok(weatherregisterform);
        //    }
        //    return BadRequest("Registration Error");
        //}
        [HttpPost("update")]
        public async Task<IActionResult> ReceiveWeatherForecastUpdate(Dictionary<string, WeatherForecastHub> newUpdate)
        {
            foreach (var item in newUpdate)
            {
                try
                {
                    _currentWeatherForecast[item.Key] = item.Value;
                }
                catch (Exception ex)
                {

                    BadRequest(ex.Message);
                }

            }
            await _hub.Clients.All.SendAsync("receiveweatherupdate", new WeatherForecast
            {
                Date = DateTime.Now,
                //TemperatureC = _currentWeatherForecast["temperature c : "],
                //Summary = _currentWeatherForecast["summary : "],
                //Description = _currentWeatherForecast["description : "],
                //Humidity = _currentWeatherForecast["humidity : "],
                //Precipitation = _currentWeatherForecast["precipitation : "]
            });
            return Ok(_currentWeatherForecast);
        }
    }
}
