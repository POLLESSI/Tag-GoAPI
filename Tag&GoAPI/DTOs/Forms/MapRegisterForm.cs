using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class MapRegisterForm
    {
    #nullable disable
        [Required(ErrorMessage = "Date of creation is required")]
        [DisplayName("Date Creation : ")]
        public DateTime DateCreation { get; set; }
        [Required(ErrorMessage = "Url is required")]
        [MinLength(16)]
        [MaxLength(2048)]
        [DisplayName("Map Url : ")]
        public string MapUrl { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [MinLength(2)]
        [MaxLength(64)]
        public string Description { get; set; }
    }
}
