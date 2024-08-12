using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class LoginNUser
    {
    #nullable disable
        [Required(ErrorMessage = "Email is required !!! ")]
        [MaxLength(64)]
        [EmailAddress]
        [DisplayName("Email : ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The password is required !!! ")]
        [MaxLength(64)]
        [DisplayName("Password : ")]
        public string Pwd { get; set; }
    }
}
