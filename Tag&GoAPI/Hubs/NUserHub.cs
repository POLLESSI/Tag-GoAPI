using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class NUserHub : Hub
    {
    #nullable disable
        private static List<NUser> _nUsers = new List<NUser>();
        public async Task RefreshNUser()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewnuser");
            }
        }
    }
}
