
namespace Tag_Go.DAL.Entities
{
    public class NVote
    {
#nullable disable
        public int NVote_Id { get; set; }
        public int NEvenement_Id { get; set; }
        public string FunOrNot { get; set; }
        public string Comment { get; set; }
        public bool Active { get; set; }
    }
}
