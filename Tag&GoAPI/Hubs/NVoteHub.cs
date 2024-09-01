using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class NVoteHub : Hub
    {
    #nullable disable
        private static List<NVote> _nVotes = new List<NVote>();
        public async Task RefreshVote()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewvote");
            }
        }
    }
}
