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
        bool Create(Recompense recompense);
        void CreateRecompense(Recompense recompense);
        IEnumerable<Recompense?> GetAllRecompenses();
        Recompense? GetByIdRecompense(int recompense_Id);
        Recompense? DeleteRecompense(int recompense_Id);
        Recompense? UpdateRecompense(string definition, string point, string implication, string granted, int recompense_Id);
    }
}
