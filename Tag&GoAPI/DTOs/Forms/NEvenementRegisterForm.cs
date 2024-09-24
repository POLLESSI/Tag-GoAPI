using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class NEvenementRegisterForm
    {
    #nullable disable
        [Required(ErrorMessage = "The date is required")]
        [DisplayName("Evenement Date : ")]
        public DateTime NEvenementDate { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Event's name : ")]
        public string NEvenementName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        [DisplayName("Event's description : ")]
        public string NEvenementDecription { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        [DisplayName("Latitude : ")]
        public string PosLat { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        [DisplayName("Longitude : ")]
        public string PosLong { get; set; }
        [Required]
        [DisplayName("Positif? Yes? No? : ")]
        public string Positif { get; set; }
        [Required]
        [DisplayName("Id Organisateur : ")]
        public int Organisateur_Id { get; set; }
        [Required]
        [DisplayName("Id Iconne : ")]
        public int NIcon_Id { get; set; }
        [Required]
        [DisplayName("Id Recompense : ")]
        public int Recompense_Id { get; set; }
        [Required]
        [DisplayName("Id Bonus : ")]
        public int Bonus_Id { get; set; }
        [Required]
        [DisplayName("Id Media Item : ")]
        public int MediaItem_Id { get; set; }
    }
}
