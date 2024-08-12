using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class MediaItemRegisterForm
    {
    #nullable disable
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("Media Type : ")]
        public string MediaType { get; set; }
        [Required]
        [MinLength(2)]
        [DisplayName("Url's media : ")]
        public string UrlItem { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(256)]
        [DisplayName("Description : ")]
        public string Description { get; set; }
    }
}
