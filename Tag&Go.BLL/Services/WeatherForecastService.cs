using Tag_Go.DAL.Repositories;
using Tag_Go.BLL;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Tag_Go.BLL.Interfaces;
using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Interfaces;

namespace Tag_Go.BLL.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
    #nullable disable
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public WeatherForecastService(IWeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        public bool Create(WeatherForecast wearherForecast)
        {
            try
            {
                return _weatherForecastRepository.Create(wearherForecast);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating weather forecast: {ex.ToString}");
            }
            return false;
        }

        public void CreateWearherForecast(WeatherForecast weatherForecast)
        {
            try
            {
                _weatherForecastRepository.CreateWeatherForecast(weatherForecast);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Creation Weather Forecast: {ex.ToString}");
            }
        }

        public Task<WeatherForecast?> DeleteWeatherForecast(int weatherForecast_Id)
        {
            try
            {
                return _weatherForecastRepository.DeleteWeatherForecast(weatherForecast_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"error deleting weather forecast: {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<WeatherForecast>> GetAllWeatherForecasts()
        {
            return _weatherForecastRepository.GetAllWeatherForecasts();
        }

        public Task<WeatherForecast?> GetByIdWeatherForecast(int weatherForecast_Id)
        {
            try
            {
                return _weatherForecastRepository.GetByIdWeatherForecast(weatherForecast_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting weather forecast: {ex.ToString}");
            }
            return null;
        }

        public Task<WeatherForecast?> UpdateWeatherForecast(WeatherForecast weatherForecast)
        {
            try
            {
                var updateWeatherForecast = _weatherForecastRepository.UpdateWeatherForecast(weatherForecast);
                return updateWeatherForecast;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating weather forecast: {ex}");
            }
            return null;
        }
    }
}
