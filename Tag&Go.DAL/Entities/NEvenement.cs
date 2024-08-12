
namespace Tag_Go.DAL.Entities
{
    public class NEvenement
    {
    #nullable disable
        public int NEvenement_Id { get; set; }
        public DateTime NEvenementDate { get; set; }
        public string NEvenementName { get; set; }
        public string NEvenementDescription { get; set; }
        public string PosLat { get; set; }
        public string PosLong { get; set; }
        public string Positif { get; set; }
        public int Organisateur_Id { get; set; }
        public int NIcon_Id { get; set; }
        public int Recompense_Id { get; set; }
        public int Bonus_Id { get; set; }
        public int MediaItem_Id { get; set; }
        public bool Active { get; set; }
    }
}
