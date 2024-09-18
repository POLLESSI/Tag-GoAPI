using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tag_GoAPI.DTOs.Forms
{
    public class WeatherForecastUpdate
    {
#nullable disable
        [Required(ErrorMessage = "Weather Forecast Id is required")]
        [DisplayName("Id Weather Forecast : ")]
        public int WeatherForecast_Id { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayName("Date : ")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Celcius Temperatures are required ! ")]
        [MinLength(2)]
        [MaxLength(4)]
        [DisplayName("Temperature C° : ")]
        public string TemperatureC { get; set; }
        [MaxLength(4)]
        [DisplayName("Temperature F° : ")]
        public string TemperatureF { get; set; }
        [Required(ErrorMessage = "The Summary is required")]
        [MinLength(2)]
        [MaxLength(256)]
        [DisplayName("Summary : ")]
        public string Summary { get; set; }
        [Required(ErrorMessage = "Description is required ! ")]
        [MinLength(4)]
        [MaxLength(256)]
        [DisplayName("Description : ")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Humidity is required ! ")]
        [MinLength(2)]
        [MaxLength(8)]
        [DisplayName("Humidity : ")]
        public string Humidity { get; set; }
        [Required(ErrorMessage = "Prescription is required ! ")]
        [MinLength(2)]
        [MaxLength(8)]
        [DisplayName("Precipitation : ")]
        public string Precipitation { get; set; }
        [Required(ErrorMessage = "Id Event is required ! ")]
        [DisplayName("Id Event : ")]
        public int NEvenement_Id { get; set; }
    }
}
