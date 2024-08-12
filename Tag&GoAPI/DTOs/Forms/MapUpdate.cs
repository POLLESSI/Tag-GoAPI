using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class MapUpdate
    {
    #nullable disable
        [Required]
        [DisplayName("Id map : ")]
        public int Map_Id { get; set; }
        [Required]
        [DisplayName("Date of creation : ")]
        public DateTime DateCreation { get; set; }
        [Required]
        [MinLength(16)]
        [MaxLength(2048)]
        [DisplayName("Map Url : ")]
        public string MapUrl { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Description : ")]
        public string Description { get; set; }
    }
}
