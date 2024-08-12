using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tag_GoAPI.DTOs.Forms
{
    public class NVoteRegisterForm
    {
    #nullable disable
        [Required]
        [DisplayName("Id Event : ")]
        public int NEvenement_Id { get; set; }
        [Required]
        [DisplayName("Fun ? Y for Yes, N for Not : ")]
        [MaxLength(1)]
        public string FunOrNot { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(128)]
        [DisplayName("Comments : ")]
        public string Comment { get; set; }
    }
}
