using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class NUserRegisterForm
    {
    #nullable disable
        [Required(ErrorMessage = "Email address is required !!! ")]
        [EmailAddress]
        [MinLength(8)]
        [MaxLength(64)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address : ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8)]
        [MaxLength(64)]
        [DisplayName("Password : ")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\-\.=+*@?]).*$", ErrorMessage = "The format is too simple for security.")]
        public string Pwd { get; set; }
        [DisplayName("Please confirm your password !")]
        [DataType(DataType.Password)]
        [Compare(nameof(Pwd))]
        public string SecondPassword { get; set; }
        [Required(ErrorMessage = "Person's Id is required !!!")]
        [DisplayName("Person's Id : ")]
        public int NPerson_Id { get; set; }
        [Required(ErrorMessage = "Rôle's Id is required !!!! ")]
        [MaxLength(1)]
        [DisplayName("Rôle's Id : ")]
        public string Role_Id { get; set; }
        [Required]
        [DisplayName("Avatar Id : ")]
        public int Avatar_Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(8)]
        [DisplayName("Points : ")]
        public string Point { get; set; }
    }
}
