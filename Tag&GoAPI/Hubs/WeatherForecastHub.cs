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
        public async Task RefreshWeatherForecast()
        {
            if (Clients is not null)
                await Clients.All.SendAsync("notifynewweatherforecast");
        }
    }
}
