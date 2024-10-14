using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.Avatar;

namespace Tag_Go.BLL.Interfaces
{
    public interface IAvatarService
    {
    #nullable disable
        Task<Avatar> Create(Avatar avatar);
        void CreateAvatar(Avatar avatar);
        Task <IEnumerable<Avatar?>> GetAllAvatars(bool includeInactive = false);
        Task<Avatar?> GetByIdAvatar(int avatar_Id);
        Task<Avatar?> DeleteAvatar(int avatar_Id);
        Task<Avatar?> UpdateAvatar(Avatar avatar);
    }
}
