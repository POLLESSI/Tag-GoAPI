using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class NPersonHub : Hub
    {
    #nullable disable
        private static List<NPerson> _nPersons = new List<NPerson>();
        public async Task RefreshPerson()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewnperson");
            }
        }
    }
}
