
namespace Tag_Go.DAL.Entities
{
    public class Chat
    {
    #nullable disable
        public int Chat_Id { get; set; }
        public string NewMessage { get; set; }
        public string Author { get; set; }
        public int Evenement_Id { get; set; }
        public int Activity_Id { get; set; }
        public bool Active { get; set; }
    }
}
