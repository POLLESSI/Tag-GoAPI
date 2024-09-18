using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.WeatherForecast;

namespace Tag_Go.BLL.Interfaces
{
    public interface IWeatherForecastService
    {
    #nullable disable
        bool Create(WeatherForecast wearherForecast);
        void CreateWearherForecast(WeatherForecast weatherForecast);
        Task<IEnumerable<WeatherForecast>> GetAllWeatherForecasts();
        Task<WeatherForecast?> GetByIdWeatherForecast(int weatherForecast_Id);
        Task<WeatherForecast?> DeleteWeatherForecast(int weatherForecast_Id);
        Task<WeatherForecast?> UpdateWeatherForecast(WeatherForecast weatherForecast);
    }
}
