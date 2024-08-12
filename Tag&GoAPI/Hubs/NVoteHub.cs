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

        public async Task SubmitNVote(NVote nVote)
        {
            _nVotes.Add(nVote);
            await Clients.All.SendAsync("ReceiveNVote", nVote);
        }

        public async Task RefreshVote()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewvote");
            }
        }

        public async Task DeleteNVote(int nVote_Id)
        {
            var nvote = _nVotes.FirstOrDefault(nv => nv.NVote_Id == nVote_Id);
            if (Clients is not null)
            {
                _nVotes.Remove(nvote);
                await Clients.All.SendAsync("NVoteDeleted", nVote_Id);
            }
        }
        public async Task GetAllNVotes()
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveAllNVotes", _nVotes);
        }
        public async Task GetByIdNVote(int nVote_Id)
        {
            var nvote = _nVotes.FirstOrDefault(nv => nv.NVote_Id == nVote_Id);
            if (Clients is not null)
                await Clients.All.SendAsync("ReceiveNVote", nvote);
        }
    }
}
