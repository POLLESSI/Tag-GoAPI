namespace Tag_GoAPI.DTOs
{
    public class WeatherForecastDTO
    {
    #nullable disable
        public DateTime Date { get; set; }
        public string TemperatureC { get; set; }
        public string TemperatureF { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Humidity { get; set; }
        public string Precipitation { get; set; }
        public int NEvenement_Id { get; set; }
        public bool Active { get; set; }
        public int WeatherForecast_Id { get; set; }
    }
}
