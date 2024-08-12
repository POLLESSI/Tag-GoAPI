using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class NIconRegisterForm
    {
    #nullable disable
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("Icon Name : ")]
        public string NIconName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(128)]
        [DisplayName("Icon Description : ")]
        public string NIconDescription { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(2048)]
        [DisplayName("Icon Url : ")]
        public string NIconUrl { get; set; }
    }
}
