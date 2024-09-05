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
        private readonly Dictionary<string, WeatherForecastHub> _currentWeatherForecast = new Dictionary<string, WeatherForecastHub>();

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
                var weatherForecasts = await _forecastRepository.GetAllWeatherForecasts();
                return Ok(weatherForecasts);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        //[HttpGet("{weatherForecast_Id}")]
        //public IActionResult GetByIdWeatherForecast(int weatherForecast_Id)
        //{
        //    try
        //    {
        //        var weatherforecast = _forecastRepository.GetByIdWeatherForecast(weatherForecast_Id);
        //        if (!ModelState.IsValid)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(_forecastRepository.GetByIdWeatherForecast(weatherForecast_Id));
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(StatusCodes.Status404NotFound, ex.Message);
        //    }

        //}
        [HttpPost]
        public async Task<IActionResult> Create(WeatherForecastRegisterForm weatherregisterform)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (_forecastRepository.Create(weatherregisterform.WeatherForecastToDal()))
            {
                await _hub.RefreshWeatherForecast();
                return Ok(weatherregisterform);
            }
            return BadRequest("Registration Error");
        }
        //[HttpDelete("{weatherForecast_Id}")]
        //public async Task<IActionResult> DeleteWeatherForecast(int weatherForecast_Id)
        //{
        //    try
        //    {
        //        var weatherforecast = await _forecastRepository.DeleteWeatherForecast(weatherForecast_Id);
        //        if (!ModelState.IsValid)
        //        {
        //            return NotFound();
        //        }
        //        return Ok("Deleted");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //[HttpPut("{weatherForecast_Id}")]
        //public async Task<IActionResult> UpdateWeatherForecast(int weatherForecast_Id, DateTime Date, string temperatureC, string temperatureF, string summary, string description, string humidity, string precipitation, int nEvenement_Id)
        //{
        //    try
        //    {
        //        _forecastRepository.UpdateWeatherForecast(weatherForecast_Id, Date, temperatureC, temperatureF, summary, description, humidity, precipitation, nEvenement_Id);
        //        return Ok("Updated");
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //[HttpPost("update")]
        //public async Task<IActionResult> ReceiveWeatherForecastUpdate(Dictionary<string, WeatherForecastHub> newUpdate)
        //{
        //    foreach (var item in newUpdate)
        //    {
        //        try
        //        {
        //            _currentWeatherForecast[item.Key] = item.Value;
        //        }
        //        catch (Exception ex)
        //        {

        //            BadRequest(ex.Message);
        //        }

        //    }
        //    return Ok(_currentWeatherForecast);
        //}
    }
}
