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
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
#nullable disable
        private readonly IWeatherForecastRepository _forecastRepository;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastHub _hub;
        
        public WeatherForecastController(IWeatherForecastRepository forecastRepository, ILogger<WeatherForecastController> logger, WeatherForecastHub hub)
        {
            _forecastRepository = forecastRepository;
            _logger = logger;
            _hub = hub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWeatherForecasts()
        {
            try
            {
                bool isAdmin = User.IsInRole("Admin");
                var weatherForecasts = await _forecastRepository.GetAllWeatherForecasts(isAdmin);

                if (!weatherForecasts.Any()) 
                {
                    return NotFound("No active Weathers Forecasts found.");
                }

                return Ok(weatherForecasts);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}" );
            }

        }
        [HttpGet("{weatherForecast_Id}")]
        public IActionResult GetByIdWeatherForecast(int weatherForecast_Id)
        {
            try
            {
                var weatherforecast = _forecastRepository.GetByIdWeatherForecast(weatherForecast_Id);
                if (weatherforecast == null)
                {
                    return NotFound($"Weather Forecast with ID {weatherForecast_Id} not found");
                }
                return Ok(weatherforecast);
            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving Weather Forecast: {ex.Message}");
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(WeatherForecastRegisterForm weatherregisterform)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var weatherForecastDal = weatherregisterform.WeatherForecastToDal();
                Tag_Go.DAL.Entities.WeatherForecast weatherForecastCreated = await _forecastRepository.Create(weatherForecastDal);

                if (weatherForecastCreated != null)
                {
                    await _hub.RefreshWeatherForecast();

                    return CreatedAtAction(nameof(Create), new { id = weatherForecastCreated.WeatherForecast_Id }, weatherForecastCreated);
                }
                return BadRequest(new { message = "Registration Error. Could not create weather forecast" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating Weather Forecast; {ex}");
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
            
        }
        [HttpDelete("{weatherForecast_Id}")]
        public async Task<IActionResult> DeleteWeatherForecast(int weatherForecast_Id)
        {
            try
            {
                var weatherforecast = await _forecastRepository.DeleteWeatherForecast(weatherForecast_Id);
                if (weatherforecast == null)
                {
                    return NotFound($"Weather Forecast with ID {weatherForecast_Id} not found");
                }
                return Ok("Weather Forecast deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }
        [HttpPut("{weatherForecast_Id}")]
        public async Task<IActionResult> UpdateWeatherForecast(WeatherForecastUpdate weatherForecastUpdate)
        {
            try
            {
                var weatherForecastDal = weatherForecastUpdate.WeatherForecastUpdateToDal();
                var updateWeatherForecast = await _forecastRepository.UpdateWeatherForecast(weatherForecastDal);

                if (updateWeatherForecast == null)
                {
                    return NotFound($"Weather Forecast with ID {weatherForecastDal.WeatherForecast_Id} not found.");
                }

                return Ok(updateWeatherForecast);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
