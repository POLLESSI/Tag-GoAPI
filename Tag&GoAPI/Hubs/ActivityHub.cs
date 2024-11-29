using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class ActivityHub : Hub
    {
    #nullable disable
        private static List<Activity> _activities = new List<Activity>();

        public ActivityHub()
        {
        }

        public async Task RefreshActivity()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewactivity");
            }
        }
        public async Task UpdateMarker(int nEvenement_Id, string comment, bool funOrNot)
        {
            //Notify all clients of an update
            await Clients.All.SendAsync("MarkerUpdated", nEvenement_Id, comment, funOrNot); 
        }
    }
}
