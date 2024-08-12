using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class ActivityRegisterForm
    {
    #nullable disable
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Activity Name : ")]
        public string ActivityName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Activity Address : ")]
        public string ActivityAddress { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        [DisplayName("Activity Description : ")]
        public string ActivityDescription { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Complementare Information : ")]
        public string ComplementareInformation { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(8)]
        [DisplayName("Latitude : ")]
        public string PosLat { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(9)]
        [DisplayName("Longitude : ")]
        public string PosLong { get; set; }
        [Required]
        [DisplayName("Id Organisateur : ")]
        public int Organisateur_Id { get; set; }
    }
}
