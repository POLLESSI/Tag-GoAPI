using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class ChangeRole
    {
    #nullable disable
        [Required(ErrorMessage = "Id of user is required")]
        [DisplayName("User Guid : ")]
        public int NUser_Id { get; set; }
        [Required(ErrorMessage = "Id of the new rôle is required")]
        [MaxLength(1)]
        [DisplayName("Id rôle : ")]
        public string Role_Id { get; set; }
    }
}
