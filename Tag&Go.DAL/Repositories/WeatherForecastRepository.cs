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

        public async Task <WeatherForecast> Create(WeatherForecast wearherForecast)
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
                parameters.Add("@Humidity", wearherForecast.Humidity);
                parameters.Add("@Precipitation", wearherForecast.Precipitation);
                parameters.Add("@NEvenement_Id", wearherForecast.NEvenement_Id);
                //return _connection.Execute(sql, parameters) > 0; 
                var newId = _connection.QuerySingle<int>(sql, parameters);

                wearherForecast.WeatherForecast_Id = newId;

                return wearherForecast;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding WeatherForecast: {ex.ToString}");
                return null;
            }
            
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

        public async Task<WeatherForecast?> DeleteWeatherForecast(int weatherForecast_Id)
        {
            try
            {
                string sql = "SELECT * FROM WeatherForecast WHERE WeatherForecast_Id = @weatherForecast_Id";
                DynamicParameters parameters = new DynamicParameters();

                var weatherForecast = await _connection.QueryFirstOrDefaultAsync<WeatherForecast>(sql, new { weatherForecast_Id });

                if (weatherForecast == null)
                {
                    return null;
                }

                string deleteSql = "DELETE FROM WeatherForecast WHERE WeatherForecast_Id = @weatherForecast_Id";

                await _connection.ExecuteAsync(deleteSql, parameters);
                return weatherForecast;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting weather forecast: {ex.ToString}");
                return null;
            }
            
        }

        public async Task<IEnumerable<WeatherForecast?>> GetAllWeatherForecasts(bool includeInactive = false)
        {
            try
            {
                string sql = includeInactive ? "SELECT * FROM WeatherForecast" : "SELECT * FROM WeatherForecast WHERE Active = 1";
                var weatherForecasts = await _connection.QueryAsync<WeatherForecast?>(sql);
                return weatherForecasts;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error retrieving Weathers Forecasts: {ex.Message}");
                return Enumerable.Empty<WeatherForecast>();
            }
            
        }

        public async Task<WeatherForecast?> GetByIdWeatherForecast(int weatherForecast_Id)
        {
            try
            {
                string sql = "SELECT * FROM WeatherForecast WHERE WeatherForecast_Id = @weatherForecast_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@weatherForecast_Id", weatherForecast_Id);

                var weatherForecast = await _connection.QueryFirstAsync<WeatherForecast?>(sql, parameters);

                return weatherForecast ?? null;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Weather Forecast: {ex.ToString}");
            }
            return null;
        }

        public async Task<WeatherForecast?> UpdateWeatherForecast(WeatherForecast weatherForecast)
        {
            try
            {
                string sql = @"
                    UPDATE WeatherForecast 
                    SET 
                        Date = @date,
                        TemperatureC = @temperatureC,
                        TemperatureF = @temperatureF,
                        Summary = @summary,
                        Description = @description,
                        Humidity = @humidity,
                        Precipitation = @precipitation
                    WHERE 
                        WeatherForecast_Id = @weatherForecast_Id";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@weatherForecast_Id", weatherForecast.WeatherForecast_Id);
                parameters.Add("@date", weatherForecast.Date);
                parameters.Add("@temperatureC", weatherForecast.TemperatureC);
                parameters.Add("@temperatureF", weatherForecast.TemperatureF);
                parameters.Add("@summary", weatherForecast.Summary);
                parameters.Add("@description", weatherForecast.Description);
                parameters.Add("@humidity", weatherForecast.Humidity);
                parameters.Add("@precipitation", weatherForecast.Precipitation);
                parameters.Add("@nEvenement_Id", weatherForecast.NEvenement_Id);

                await _connection.QueryFirstAsync<WeatherForecast?>(sql, parameters);

                return weatherForecast;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Weather Forecast: {ex}");
                return null;
            }
        }
    }
}
