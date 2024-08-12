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

        public BonusHub()
        {
        }

        public async Task SubmitBonus(Bonus bonus)
        {
            if (Clients is not null)
            {
                _bonuss.Add(bonus);
                await Clients.All.SendAsync("receiveBonus", bonus);
            }
        }
        public async Task RefreshBonus()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewbonus");
            }
        }
        public async Task deleteBonus(int bonus_Id)
        {
            var bonus = _bonuss.Find(bo => bo.Bonus_Id == bonus_Id);
            if (Clients is not null)
            {
                _bonuss.Remove(bonus);
                await Clients.All.SendAsync("BonusDeleted", bonus_Id);
            }
        }
        public async Task GetAllBonuss()
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveAllBonus", _bonuss);
        }
        public async Task GetByIdBonus(int bonus_Id)
        {
            var bonus = _bonuss.Find(bo => bo.Bonus_Id == bonus_Id);
            if (Clients is not null)
                await Clients.All.SendAsync("ReceiveBonus", bonus);
        }
        public async Task UpdateBonus(Bonus updateBonus)
        {
            var bonus = _bonuss.Find(bo => bo.Bonus_Id == updateBonus.Bonus_Id);
            if (Clients is not null)
            {
                bonus.BonusType = updateBonus.BonusType;
                bonus.BonusDescription = updateBonus.BonusDescription;
                bonus.Application = updateBonus.Application;
                bonus.Granted = updateBonus.Granted;
                await Clients.All.SendAsync("ReceiveBonus", bonus);
            }
        }
    }
}
