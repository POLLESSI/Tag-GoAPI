using Tag_Go.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.NPerson;

namespace Tag_Go.DAL.Interfaces
{
    public interface INPersonRepository
    {
    #nullable disable
        bool Create(NPerson nPerson);
        void CreatePerson(NPerson nPerson);
        Task<IEnumerable<NPerson?>> GetAllNPersons();
        Task<NPerson?> GetByIdNPerson(int nPerson_Id);
        Task<NPerson?> DeleteNPerson(int nPerson_Id);
        Task<NPerson?> UpdateNPerson(string lastname, string firstname, string email, string address_Street, string address_Nbr, string postalCode, string address_City, string address_Country, string telephone, string gsm, int nPerson_Id);
    }
}
