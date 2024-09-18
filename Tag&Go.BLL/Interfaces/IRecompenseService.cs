using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.Recompense;

namespace Tag_Go.BLL.Interfaces
{
    public interface IRecompenseService
    {
    #nullable disable
        bool Create(Recompense recompense);
        void CreateRecompense(Recompense recompense);
        Task<IEnumerable<Recompense?>> GetAllRecompenses();
        Task<Recompense?> GetByIdRecompense(int recompense_Id);
        Task<Recompense?> DeleteRecompense(int recompense_Id);
        Task<Recompense?> UpdateRecompense(Recompense recompense);
    }
}
