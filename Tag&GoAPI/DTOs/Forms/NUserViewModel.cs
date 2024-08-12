using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class NUserViewModel
    {
    #nullable disable
        public int NUser_Id { get; set; }
        [Required(ErrorMessage = "The nick name is required")]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Nick name : ")]
        public string Pseudo { get; set; }
        [Required(ErrorMessage = " The password is required")]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Password : ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        [MinLength(8)]
        [MaxLength(64)]
        [DisplayName("Email address ; ")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Rôle Id : ")]
        public bool Role_Id { get; set; }
        [Required]
        [DisplayName("Avatar Id : ")]

        public int Avatar_Id { get; set; }
        [Required]
        [DisplayName("Point : ")]
        public string Point { get; set; }
    }
}
