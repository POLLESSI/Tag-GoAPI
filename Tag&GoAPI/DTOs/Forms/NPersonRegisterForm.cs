using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class NPersonRegisterForm
    {
    #nullable disable
        [Required(ErrorMessage = "The last name is required")]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("Last name : ")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "The first name is required")]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("First name : ")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Email Address is required")]
        [MinLength(8)]
        [MaxLength(64)]
        [DisplayName("Email : ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Street is required")]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Street : ")]
        public string Address_Street { get; set; }
        [Required(ErrorMessage = "Numero is required")]
        [MaxLength(8)]
        [DisplayName("Numero : ")]
        public string Address_Nbr { get; set; }
        [Required(ErrorMessage = "Postal Code is required")]
        [MaxLength(8)]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "The name of the city is required")]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("City : ")]
        public string Address_City { get; set; }
        [Required(ErrorMessage = "The name of the country is required")]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Country : ")]
        public string Address_Country { get; set; }
        [MinLength(8)]
        [MaxLength(16)]
        [DisplayName("Telephone : ")]
        public string Telephone { get; set; }
        [MinLength(8)]
        [MaxLength(16)]
        [DisplayName("Gsm : ")]
        public string Gsm { get; set; }
    }
}
