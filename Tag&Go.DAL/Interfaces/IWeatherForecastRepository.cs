using Tag_Go.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.WeatherForecast;

namespace Tag_Go.DAL.Interfaces
{
    public interface IWeatherForecastRepository
    {
    #nullable disable
        bool Create(WeatherForecast wearherForecast);
        void CreateWeatherForecast(WeatherForecast weatherForecast);
        Task<IEnumerable<WeatherForecast?>> GetAllWeatherForecasts();
        Task<WeatherForecast?> GetByIdWeatherForecast(int weatherForecast_Id);
        Task<WeatherForecast?> DeleteWeatherForecast(int weatherForecast_Id);
        Task<WeatherForecast?> UpdateWeatherForecast(int weatherForecast_Id, DateTime date, string temperatureC, string temperatureF, string summary, string description, string humidity, string precipitation, int nEvenement_Id);
    }
}
