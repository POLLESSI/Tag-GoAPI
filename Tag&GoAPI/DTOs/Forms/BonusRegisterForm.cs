using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class BonusRegisterForm
    {
    #nullable disable
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("Bonus Type : ")]
        public string BonusType { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        [DisplayName("Description : ")]
        public string BonusDescription { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Application : ")]
        public string Application { get; set; }
        [Required]
        [DisplayName("Granted? : ")]
        public string Granted { get; set; }
    }
}
