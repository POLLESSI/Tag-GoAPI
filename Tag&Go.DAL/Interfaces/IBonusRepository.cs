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
        Task<IEnumerable<Bonus?>> GetAllBonuss();
        Task<Bonus?> GetByIdBonus(int bonus_Id);
        Task<Bonus?> DeleteBonus(int bonus_Id);
        Task<Bonus?> UpdateBonus(int bonus_Id, string bonusType, string bonusDescription, string application, string granted);
    }
}
