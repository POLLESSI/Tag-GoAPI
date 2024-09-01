using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class NIconHub : Hub
    {
    #nullable disable
        private static List<NIcon> _nIcons = new List<NIcon>();
        public async Task RefreshIcon()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewnicon");
            }
        }
    }
}
