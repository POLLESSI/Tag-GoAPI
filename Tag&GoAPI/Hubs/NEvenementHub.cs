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

        public NEvenementHub()
        {
        }

        public async Task SubmitNEvenement(NEvenement nEvenement)
        {
            if (Clients is not null)
            {
                _nEvenements.Add(nEvenement);
                await Clients.All.SendAsync("ReceiveNEvenement", nEvenement);
            }
        }
        public async Task RefreshEvenement()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewnevenement");
            }
        }
        public async Task DeleteNEvenement(int nEvenement_Id)
        {
            var nevenement = _nEvenements.Find(nev => nev.NEvenement_Id == nEvenement_Id);
            if (Clients is not null)
            {
                _nEvenements.Remove(nevenement);
                await Clients.All.SendAsync("NEvenementDeleted", nEvenement_Id);
            }

        }
        public async Task GetAllNEvenements()
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveAllNEvenement", _nEvenements);
        }
        public async Task GetByIdNEvenement(int nEvenement_Id)
        {
            var nevenement = _nEvenements.Find(nev => nev.NEvenement_Id == nEvenement_Id);
            if (Clients is not null)

                await Clients.Caller.SendAsync("ReceiveNEvenement", nevenement);
        }
        public async Task UpdateNEvenement(NEvenement updatedNEvenement)
        {
            var nevenement = _nEvenements.Find(nev => nev.NEvenement_Id == updatedNEvenement.NEvenement_Id);
            if (Clients is not null)
            {
                nevenement.NEvenementDate = updatedNEvenement.NEvenementDate;
                nevenement.NEvenementName = updatedNEvenement.NEvenementName;
                nevenement.NEvenementDescription = updatedNEvenement.NEvenementDescription;
                nevenement.PosLat = updatedNEvenement.PosLat;
                nevenement.PosLong = updatedNEvenement.PosLong;
                nevenement.Positif = updatedNEvenement.Positif;
                nevenement.Organisateur_Id = updatedNEvenement.Organisateur_Id;
                nevenement.NIcon_Id = updatedNEvenement.NIcon_Id;
                nevenement.Recompense_Id = updatedNEvenement.Recompense_Id;
                nevenement.Bonus_Id = updatedNEvenement.Bonus_Id;
                nevenement.MediaItem_Id = updatedNEvenement.MediaItem_Id;
                await Clients.All.SendAsync("ReceiveNEvenement", nevenement);
            }
        }
    }
}
