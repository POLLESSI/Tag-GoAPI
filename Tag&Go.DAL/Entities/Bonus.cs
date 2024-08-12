
namespace Tag_Go.DAL.Entities
{
    public class Bonus
    {
    #nullable disable
        public int Bonus_Id { get; set; }
        public string BonusType { get; set; }
        public string BonusDescription { get; set; }
        public string Application { get; set; }
        public string Granted { get; set; }
        public bool Active { get; set; }
    }
}
