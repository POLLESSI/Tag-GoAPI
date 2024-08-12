using Tag_GoAPI.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tag_GoAPI.Hubs
{
    public class WeatherForecastHub : Hub
    {
    #nullable disable
        private static List<WeatherForecast> _forecasts = new List<WeatherForecast>();

        public WeatherForecastHub()
        {
        }

        public async Task SubmitWeatherForecast(WeatherForecast weatherForecast)
        {
            //  Send weather update to all connected clients
            if (Clients is not null)
            {
                _forecasts.Add(weatherForecast);
                await Clients.All.SendAsync("ReceiveWeatherforecast", weatherForecast);
            }
        }
        public async Task RefreshWeatherForecast()
        {
            if (Clients is not null)
                await Clients.All.SendAsync("notifynewweatherforecast");
        }

        //public async Task DeleteWeatherForecast(int weatherForecast_Id)
        //{
        //    var weatherforecast = _forecasts.FirstOrDefault(we => we.WeatherForecast_Id == weatherForecast_Id);
        //    if (Clients is not null)
        //    {
        //        _forecasts.Remove(weatherforecast);
        //        await Clients.All.SendAsync("WeatherForecastDeleted", weatherForecast_Id);
        //    }
        //}
        public async Task GetAllWeatherForecasts()
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveAllWeatherforecasts", _forecasts);
        }
        //public async Task GetByIdWeatherForecast(int weatherForecast_Id)
        //{
        //    var weatherforecast = _forecasts.Find(we => we.WeatherForecast_Id == weatherForecast_Id);
        //    if (Clients is not null)
        //        await Clients.Caller.SendAsync("ReceiveWeatherForecast", weatherforecast);
        //}
        //public async Task UpdateWeatherForecast(WeatherForecast updatedWeatherForecast)
        //{
        //    var weatherforecast = _forecasts.Find(we => we.WeatherForecast_Id == updatedWeatherForecast.WeatherForecast_Id);
        //    if (weatherforecast is not null)
        //    {
        //        weatherforecast.Date = updatedWeatherForecast.Date;
        //        weatherforecast.TemperatureC = updatedWeatherForecast.TemperatureC;
        //        weatherforecast.TemperatureF = updatedWeatherForecast.TemperatureF;
        //        weatherforecast.Summary = updatedWeatherForecast.Summary;
        //        weatherforecast.Description = updatedWeatherForecast.Description;
        //        weatherforecast.Humidity = updatedWeatherForecast.Humidity;
        //        weatherforecast.Precipitation = updatedWeatherForecast.Precipitation;

        //        await Clients.All.SendAsync("ReceiveWeatherForecast", weatherforecast);
        //    }
        //}
    }
}
