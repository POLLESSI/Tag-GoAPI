using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class NIconUpdate
    {
    #nullable disable
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("Icon name : ")]
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
        [Required]
        [DisplayName("Icon_Id : ")]
        public int Icon_Id { get; set; }
    }
}
