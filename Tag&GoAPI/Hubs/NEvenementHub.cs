using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class NEvenementHub : Hub
    {
    #nullable disable
        private static List<NEvenement> _nEvenements = new List<NEvenement>();
        public async Task RefreshEvenement()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewnevenement");
            }
        }
    }
}
