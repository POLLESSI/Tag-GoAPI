using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class MapHub : Hub
    {
    #nullable disable
        private static List<Map> _maps = new List<Map>();
        public async Task RefreshMap()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewmap");
            }
        }
    }
}
