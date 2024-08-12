using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Interfaces;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Dapper;

namespace Tag_Go.DAL.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public WeatherForecastRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool Create(WeatherForecast wearherForecast)
        {
            try
            {
                string sql = "INSERT INTO WeatherForecast (Date, TemperatureC, TemperatureF, Summary, Description, Humidity, Precipitation, NEvenement_Id) VALUES " +
                    "(@Date, @TemperatureC, @TemperatureF, @Summary, @Description, @Humidity, @Precipitation, @NEvenement_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Date", wearherForecast.Date);
                parameters.Add("@TemperatureC", wearherForecast.TemperatureC);
                parameters.Add("TemperatureF", wearherForecast.TemperatureF);
                parameters.Add("@Summary", wearherForecast.Summary);
                parameters.Add("@Description", wearherForecast.Description);
                parameters.Add("@Prescription", wearherForecast.Precipitation);
                parameters.Add("@NEvenement_Id", wearherForecast.NEvenement_Id);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding WeatherForecast: {ex.ToString}");
            }
            return false;
        }

        public void CreateWeatherForecast(WeatherForecast weatherForecast)
        {
            try
            {
                string sql = "INSERT INTO WeatherForecast (Date, TemperatureC, TemperatureF, Summary, Description, Humidity, Precipitation, NEvenement_Id)" +
                    " VALUES (@date, @temperatureC, @temperatureF, @summary, @description, @humidity, @precription, @nEvenement_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@date", weatherForecast.Date);
                parameters.Add("@temperatureC", weatherForecast.TemperatureC);
                parameters.Add("@temperatureF", weatherForecast.TemperatureF);
                parameters.Add("@summary", weatherForecast.Summary);
                parameters.Add("description", weatherForecast.Description);
                parameters.Add("@humidity", weatherForecast.Humidity);
                parameters.Add("@precipitation", weatherForecast.Precipitation);
                parameters.Add("@nEvenement_Id", weatherForecast.NEvenement_Id);

                _connection.Query(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Creating Error: {ex.ToString}");
            }
        }

        public WeatherForecast? DeleteWeatherForecast(int weatherForecast_Id)
        {
            try
            {
                string sql = "DELETE FROM WeatherForecast WHERE WeatherForecast_Id = @weatherForecast_Id";
                DynamicParameters parameters = new DynamicParameters();
                return _connection.QueryFirst<WeatherForecast>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting weather forecast: {ex.ToString}");
            }
            return null;
        }

        public IEnumerable<WeatherForecast?> GetAllWeatherForecasts()
        {
            string sql = "SELECT * FROM WeatherForecast";
            return _connection.Query<WeatherForecast?>(sql);
        }

        public WeatherForecast? GetByIdWeatherForecast(int weatherForecast_Id)
        {
            try
            {
                string sql = "SELECT * FROM WeatherForecast WHERE WeatherForecast_Id = @weatherForecast_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@weatherForecast_Id", weatherForecast_Id);
                return _connection.QueryFirst<WeatherForecast?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Weather Forecast: {ex.ToString}");
            }
            return null;
        }

        public WeatherForecast? UpdateWeatherForecast(int weatherForecast_Id, DateTime date, string temperatureC, string temperatureF, string summary, string description, string humidity, string precipitation, int nEvenement_Id)
        {
            try
            {
                string sql = "UPDATE WeatherForecast SET WeatherForecast_Id = @weatherForecast_Id WHERE WeatherForecast_Id = @weatherForecast_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@weatherForecast_Id", weatherForecast_Id);
                parameters.Add("@date", date);
                parameters.Add("@temperatureC", temperatureC);
                parameters.Add("@temperatureF", temperatureF);
                parameters.Add("@summary", summary);
                parameters.Add("@description", description);
                parameters.Add("@humidity", humidity);
                parameters.Add("@precipitation", precipitation);
                parameters.Add("@nEvenement_Id", nEvenement_Id);

                return _connection.QueryFirst<WeatherForecast?>(sql, parameters);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Weather Forecast: {ex}");
            }
            return new WeatherForecast();
        }
    }
}
