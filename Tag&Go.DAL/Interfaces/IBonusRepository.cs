using Tag_Go.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.Bonus;

namespace Tag_Go.DAL.Interfaces
{
    public interface IBonusRepository
    {
    #nullable disable
        bool Create(Bonus bonus);
        void CreateBonus(Bonus bonus);
        IEnumerable<Bonus?> GetAllBonuss();
        Bonus? GetByIdBonus(int bonus_Id);
        Bonus? DeleteBonus(int bonus_Id);
        Bonus? UpdateBonus(int bonus_Id, string bonusType, string bonusDescription, string application, string granted);
    }
}
