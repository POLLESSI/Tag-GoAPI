using Tag_Go.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.Organisateur;

namespace Tag_Go.DAL.Interfaces
{
    public interface IOrganisateurRepository
    {
    #nullable disable
        bool Create(Organisateur organisateur);
        void CreateOrganisateur(Organisateur organisateur);
        IEnumerable<Organisateur?> GetAllOrganisateurs();
        Organisateur? GetByIdOrganisateur(int organisateur_Id);
        Organisateur? DeleteOrganisateur(int organisateur_Id);
        Organisateur? UpdateOrganisateur(string companyName, string businessNumber, int nUser_Id, string point, int organisateur_Id);
    }
}
