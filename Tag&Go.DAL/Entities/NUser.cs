
namespace Tag_Go.DAL.Entities
{
    public class NUser
    {
    #nullable disable
        public int NUser_Id { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public int NPerson_Id { get; set; }
        public string Role_Id { get; set; }
        public int Avatar_Id { get; set; }
        public string Point { get; set; }
        public bool Active { get; set; }
        public string Pseudo { get; set; }
        public string PasswordHash { get; set; }
    }
}
