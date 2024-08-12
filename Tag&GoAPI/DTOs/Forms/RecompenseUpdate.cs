using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tag_GoAPI.DTOs.Forms
{
    public class RecompenseUpdate
    {
    #nullable disable
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Definition : ")]
        public string Definition { get; set; }
        [Required]
        [MinLength(1)]
        [DisplayName("Points : ")]
        public string Point { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        [DisplayName("Implication : ")]
        public string Implication { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(4)]
        [DisplayName("Granted? : ")]
        public string Granted { get; set; }
        [Required]
        [DisplayName("Recompense Id : ")]
        public int Recompense_Id { get; set; }
    }
}
