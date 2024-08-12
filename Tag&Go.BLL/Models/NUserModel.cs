
namespace Tag_Go.BLL.Models
{
    public class NUserModel
    {
    #nullable disable
        public int NUser_Id { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public Guid SecurityStamp { get; set; }
        public int NPerson_Id { get; set; }
        public string Role_Id { get; set; }
        public int Avatar_Id { get; set; }
        public string Point { get; set; }
    }
}
