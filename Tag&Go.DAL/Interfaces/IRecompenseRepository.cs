using Tag_Go.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.Recompense;

namespace Tag_Go.DAL.Interfaces
{
    public interface IRecompenseRepository
    {
    #nullable disable
        Task<Recompense> Create(Recompense recompense);
        void CreateRecompense(Recompense recompense);
        Task<IEnumerable<Recompense?>> GetAllRecompenses(bool includeInactive = false);
        Task<Recompense?> GetByIdRecompense(int recompense_Id);
        Task<Recompense?> DeleteRecompense(int recompense_Id);
        Task<Recompense?> UpdateRecompense(Recompense recompense);
    }
}
