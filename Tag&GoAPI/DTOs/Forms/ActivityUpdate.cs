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
        [MaxLength(64)]
        [DisplayName("ActivityName : ")]
        public string ActivityName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
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
        //[MinLength(2)]
        //[MaxLength(10)]
        [DisplayName("Latitude : ")]
        public float PosLat { get; set; }
        [Required]
        //[MinLength(2)]
        //[MaxLength(10)]
        [DisplayName("Longitude : ")]
        public float PosLong { get; set; }
        [Required]
        [DisplayName("Id Organisateur : ")]
        public int Organisateur_Id { get; set; }
    }
}
