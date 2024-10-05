
namespace Tag_Go.DAL.Entities
{
    public class ChatActivity
    {
    #nullable disable
        public int ChatActivity_Id { get; set; }
        public string NewMessage { get; set; }
        public string Author { get; set; }
        public DateTime SendingDate { get; set; }
        public int Activity_Id { get; set; }
        public bool Active { get; set; }
    }
}
