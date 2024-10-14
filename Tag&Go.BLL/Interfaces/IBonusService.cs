using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.Bonus;

namespace Tag_Go.BLL.Interfaces
{
    public interface IBonusService
    {
    #nullable disable
        Task<Bonus> Create(Bonus bonus);
        void CreateBonus(Bonus bonus);
        Task<IEnumerable<Bonus?>> GetAllBonuss(bool includeInactive = false);
        Task<Bonus?> GetByIdBonus(int bonus_Id);
        Task<Bonus?> DeleteBonus(int bonus_Id);
        Task<Bonus?> UpdateBonus(Bonus bonus);
    }
}
