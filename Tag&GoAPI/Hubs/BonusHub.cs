using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class BonusHub : Hub
    {
    #nullable disable
        private static List<Bonus> _bonuss = new List<Bonus>();
        public async Task RefreshBonus()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewbonus");
            }
        }
    }
}
