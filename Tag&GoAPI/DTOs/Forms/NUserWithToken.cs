using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class NUserWithToken
    {
    #nullable disable
        public int NUser_Id { get; set; }
        [Required(ErrorMessage = "Nick name is required")]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Nick name : ")]
        public string Pseudo { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Password")]
        //[RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\-\.=+*@?]).*$", ErrorMessage = "The format is too simple for security.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress]
        [MinLength(2)]
        [MaxLength(64)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = " Person Id is required")]
        [DisplayName("Person Id")]
        public int NPerson_Id { get; set; }
        [Required(ErrorMessage = " The role id is required")]
        [DisplayName("Role_Id")]
        public bool Role_Id { get; set; }

        public string Token { get; set; }
    }
}
