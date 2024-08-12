using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class OrganisateurRegisterForm
    {
    #nullable disable
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Company Name : ")]
        public string CompanyName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(16)]
        [DisplayName("BusinessNumber : ")]
        public string BusinessNumber { get; set; }
        [Required]
        [DisplayName("Guid User : ")]
        public int NUser_Id { get; set; }
        [MinLength(1)]
        [MaxLength(8)]
        [DisplayName("Points : ")]
        public string Point { get; set; }
    }
}
