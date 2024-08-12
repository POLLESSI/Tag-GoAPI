using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class AvatarHub : Hub
    {
    #nullable disable
        private static List<Avatar> _avatars = new List<Avatar>();

        public AvatarHub()
        {
        }

        public async Task SubmitAvatar(Avatar avatar)
        {
            _avatars.Add(avatar);
            await Clients.All.SendAsync("ReceiveAvatar", avatar);
        }
        public async Task RefreshAvatar()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewavatar");
            }
        }
        public async Task DeleteAvatar(int avatar_Id)
        {
            var avatar = _avatars.Find(av => av.Avatar_Id == avatar_Id);
            if (avatar != null)
            {
                _avatars.Remove(avatar);
                await Clients.All.SendAsync("avatarDeleted", avatar_Id);
            }

        }
        public async Task GetAllAvatars(int avatar_Id)
        {
            await Clients.Caller.SendAsync("ReceiveAllavatar", _avatars);
        }
        public async Task GetByIdAvatar(int avatar_Id)
        {
            var avatar = _avatars.Find(av => av.Avatar_Id == avatar_Id);
            await Clients.Caller.SendAsync("ReceiveAvatar", avatar);
        }
        public async Task UpdateAvatar(Avatar updatedAvatar)
        {
            var avatar = _avatars.Find(av => av.Avatar_Id == updatedAvatar.Avatar_Id);
            if (avatar != null)
            {
                avatar.AvatarName = updatedAvatar.AvatarName;
                avatar.AvatarUrl = updatedAvatar.AvatarUrl;
                avatar.Description = updatedAvatar.Description;
                await Clients.All.SendAsync("ReceiveAvatar", avatar);
            }
        }
    }
}
