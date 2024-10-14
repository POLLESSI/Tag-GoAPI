using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.NUser;

namespace Tag_Go.BLL.Interfaces
{
    public interface INUserService
    {
    #nullable disable
        Task<NUser> Create(NUser nUser);
        void CreateNUser(NUser nUser);
        Task<IEnumerable<NUser?>> GetAllNUsers(bool includeInactive = false);
        Task<NUser?> GetByIdNUser(int nUser_Id);
        Task<NUser?> DeleteNUser(int nUser_Id);
        Task<NUser?> UpdateNUser(NUser nUser);
        bool RegisterNUser(string? email, string? pwd, int nPerson_Id, string? role_Id, int avatar_Id, string? point);
        Task<NUser?> LoginNUser(string? email, string? pwd);
        void SetRole(int nUser_Id, string? role_Id);
    }
}
