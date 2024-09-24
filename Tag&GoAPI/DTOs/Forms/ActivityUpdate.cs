using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tag_GoAPI.DTOs.Forms
{
    public class ActivityUpdate
    {
#nullable disable
        [Required]
        [DisplayName("Id Activity : ")]
        public int Activity_Id { get; set; }
        [Required]
        [DisplayName("ActivityName : ")]
        public string ActivityName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("Avatar Address : ")]
        public string ActivityAddress { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(2048)]
        [DisplayName("Activity Description: ")]
        public string ActivityDescription { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(2048)]
        [DisplayName("Complementare Information : ")]
        public string ComplementareInformation { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        [DisplayName("Latitude : ")]
        public string PosLat { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        [DisplayName("Longitude : ")]
        public string PosLong { get; set; }
        [Required]
        [DisplayName("Id Organisateur : ")]
        public int Organisateur_Id { get; set; }
    }
}
