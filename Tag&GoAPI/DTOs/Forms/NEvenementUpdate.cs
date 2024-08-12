using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tag_GoAPI.DTOs.Forms
{
    public class NEvenementUpdate
    {
    #nullable disable
        [Required]
        [DisplayName("Evenement date : ")]
        public DateTime NEvenementDate { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        [DisplayName("Event name : ")]
        public string NEvenementName { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        [DisplayName("Event description : ")]
        public string NEvenementDescription { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(8)]
        [DisplayName("Latitude : ")]
        public string PosLat { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(9)]
        [DisplayName("Longitude : ")]
        public string PosLong { get; set; }
        [Required]
        [MaxLength(1)]
        [DisplayName("Has Positif? Y for Yes, N for No : ")]
        public string Positif { get; set; }
        [Required]
        [DisplayName("Id Organisateur : ")]
        public int Organisateur_Id { get; set; }
        [Required]
        [DisplayName("Id Icon : ")]
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
        [Required]
        [DisplayName("Id Event : ")]
        public int NEvenement_Id { get; set; }
    }
}
