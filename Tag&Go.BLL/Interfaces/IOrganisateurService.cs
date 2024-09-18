using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.Organisateur;

namespace Tag_Go.BLL.Interfaces
{
    public interface IOrganisateurService
    {
    #nullable disable
        bool Create(Organisateur organisateur);
        void CreateOrganisateur(Organisateur organisateur);
        Task<IEnumerable<Organisateur?>> GetAllOrganisateurs();
        Task<Organisateur?> GetByIdOrganisateur(int organisateur_Id);
        Task<Organisateur?> DeleteOrganisateur(int organisateur_Id);
        Task<Organisateur?> UpdateOrganisateur(Organisateur organisateur);
    }
}
