using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class RecompenseHub : Hub
    {
    #nullable disable
        private static List<Recompense> _recompenses = new List<Recompense>();
        public async Task RefreshRecompense()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewrecompense");
            }
        }
    }
}
