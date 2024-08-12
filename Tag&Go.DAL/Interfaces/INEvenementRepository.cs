using Tag_Go.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.NEvenement;

namespace Tag_Go.DAL.Interfaces
{
    public interface INEvenementRepository
    {
    #nullable disable
        bool Create(NEvenement nEvenement);
        void CreateEvenement(NEvenement nEvenement);
        IEnumerable<NEvenement?> GetAllNEvenements();
        NEvenement? GetByIdNEvenement(int nEvenement_Id);
        NEvenement? DeleteNEvenement(int nEvenement_Id);
        NEvenement? UpdateNEvenement(DateTime nEvenementDate, string nEvenementDescription, string posLat, string posLong, string positif, int organisateur_Id, int nIcon_Id, int recompense_Id, int bonus_Id, int mediaItem_Id, int nEvenement_Id);
    }
}
