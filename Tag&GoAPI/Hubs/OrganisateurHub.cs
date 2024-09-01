using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class OrganisateurHub : Hub
    {
    #nullable disable
        private static List<Organisateur> _organisateurs = new List<Organisateur>();
        public async Task RefreshOrganisateur()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifyneworganisateur");
            }
        }
    }
}
