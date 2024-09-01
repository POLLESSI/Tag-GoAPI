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
        public async Task RefreshAvatar()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewavatar");
            }
        }
    }
}
