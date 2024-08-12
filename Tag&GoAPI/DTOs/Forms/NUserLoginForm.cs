using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class NUserLoginForm
    {
    #nullable disable
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress]
        [MinLength(8)]
        [MaxLength(64)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address : ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8)]
        [MaxLength(64)]
        [DataType(DataType.Password)]
        [DisplayName("Password : ")]
        public string Pwd { get; set; }
    }
}
