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

        public RecompenseHub()
        {
        }

        public async Task SubmitRecompense(Recompense recompense)
        {
            _recompenses.Add(recompense);
            if (Clients is not null)
            {
                await Clients.All.SendAsync("ReceiveRecompense", recompense);
            }
        }
        public async Task RefreshRecompense()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewrecompense");
            }
        }
        public async Task DeleteRecompense(int recompense_Id)
        {
            var recompense = _recompenses.Find(re => re.Recompense_Id == recompense_Id);
            if (Clients is not null)
            {
                _recompenses.Remove(recompense);
                await Clients.All.SendAsync("RecompenseDilited", recompense_Id);
            }
        }

        public async Task GetAllRecompenses()
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveAllRecompenses", _recompenses);
        }
        public async Task GetByIdRecompense(int recompense_Id)
        {
            var recompense = _recompenses.Find(re => re.Recompense_Id == recompense_Id);
            if (Clients is not null)
                await Clients.All.SendAsync("ReceiveRecompense", recompense);
        }
        public async Task UpdateRecompense(Recompense updatedRecompense)
        {
            var recompense = _recompenses.Find(re => re.Recompense_Id == updatedRecompense.Recompense_Id);
            if (Clients is not null)
            {
                recompense.Definition = updatedRecompense.Definition;
                recompense.Point = updatedRecompense.Point;
                recompense.Implication = updatedRecompense.Implication;
                recompense.Granted = updatedRecompense.Granted;
                recompense.Recompense_Id = updatedRecompense.Recompense_Id;

                await Clients.All.SendAsync("ReceiveRecompense", recompense);
            }
        }
    }
}
