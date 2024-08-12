
namespace Tag_Go.DAL.Entities
{
    public class Avatar
    {
    #nullable disable
        public int Avatar_Id { get; set; }
        public string AvatarName { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
