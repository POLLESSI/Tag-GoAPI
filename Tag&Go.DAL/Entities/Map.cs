
namespace Tag_Go.DAL.Entities
{
    public class Map
    {
    #nullable disable
        public int Map_Id { get; set; }
        public DateTime DateCreation { get; set; }
        public string MapUrl { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
