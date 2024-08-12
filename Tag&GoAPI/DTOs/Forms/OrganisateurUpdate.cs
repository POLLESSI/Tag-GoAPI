using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class OrganisateurUpdate
    {
    #nullable disable
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Company Name : ")]
        public string CompanyName { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        [DisplayName("Business Number : ")]
        public string BusinessNumber { get; set; }
        [Required]
        [DisplayName("Guid User : ")]
        public int NUser_Id { get; set; }
        [MaxLength(8)]
        [DisplayName("Points : ")]
        public string Point { get; set; }
        [Required]
        [DisplayName("Id Organisator : ")]
        public int Organisateur_Id { get; set; }
    }
}
