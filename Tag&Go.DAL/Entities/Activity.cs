
namespace Tag_Go.DAL.Entities
{
    public class Activity
    {
    #nullable disable
        public int Activity_Id { get; set; }
        public string ActivityName { get; set; }
        public string ActivityAddress { get; set; }
        public string ActivityDescription { get; set; }
        public string ComplementareInformation { get; set; }
        public float PosLat { get; set; }
        public float PosLong { get; set; }
        public int Organisateur_Id { get; set; }
        public bool Active { get; set; }
    }
}
