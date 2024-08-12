namespace Tag_GoAPI.DTOs
{
    public class ActivityDTO
    {
    #nullable disable
        public string ActivityName { get; set; }
        public string ActivityAddress { get; set; }
        public string ActivityDescription { get; set; }
        public string ComplementareInformation { get; set; }
        public string Poslat { get; set; }
        public string PosLong { get; set; }
        public int Organisateur_Id { get; set; }
        public bool Active { get; set; }
    }
}
