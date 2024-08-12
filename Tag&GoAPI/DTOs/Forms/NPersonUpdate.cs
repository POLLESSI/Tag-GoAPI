using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class NPersonUpdate
    {
    #nullable disable
        [Required]
        [DisplayName("Person Id : ")]
        public int NPerson_Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("Last name : ")]
        public string Lastname { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("First name : ")]
        public string Firstname { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(64)]
        [DisplayName("Email : ")]
        public string Email { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Street : ")]
        public string Address_Street { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(8)]
        [DisplayName("Numero : ")]
        public string Address_Nbr { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(8)]
        [DisplayName("Postal Code : ")]
        public string PostalCode { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("City : ")]
        public string Address_City { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Country : ")]
        public string Address_Country { get; set; }
        [MaxLength(16)]
        [DisplayName("")]
        public string Telephone { get; set; }
        public string Gsm { get; set; }
    }
}
