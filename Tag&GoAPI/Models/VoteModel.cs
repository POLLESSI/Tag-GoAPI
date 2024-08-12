namespace Tag_GoAPI.Models
{
    public class VoteModel
    {
    #nullable disable
        public int Vote_Id { get; set; }
        public int Evenement_Id { get; set; }
        public bool FunOrNot { get; set; }
        public string Comment { get; set; }
    }
}
