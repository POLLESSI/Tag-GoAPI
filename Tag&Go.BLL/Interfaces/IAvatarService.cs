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
        bool Create(Avatar avatar);
        void CreateAvatar(Avatar avatar);
        IEnumerable<Avatar?> GetAllAvatars();
        Avatar? GetByIdAvatar(int avatar_Id);
        Avatar? DeleteAvatar(int avatar_Id);
        Avatar? UpdateAvatar(int avatar_Id, string avatarName, string avatarUrl, string description);
    }
}
