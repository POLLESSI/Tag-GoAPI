using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class NIconHub : Hub
    {
    #nullable disable
        private static List<NIcon> _nIcons = new List<NIcon>();
        public async Task SubmitNIcon(NIcon nIcon)
        {
            if (Clients is not null)
            {
                _nIcons.Add(nIcon);
                await Clients.All.SendAsync("ReceiveNIcon", nIcon);
            }
        }
        public async Task RefreshIcon()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewnicon");
            }
        }
        public async Task DeleteNIcon(int nIcon_Id)
        {
            var nicon = _nIcons.Find(ni => ni.NIcon_Id == nIcon_Id);
            if (Clients is not null)
            {
                _nIcons.Remove(nicon);
                await Clients.All.SendAsync("NIconDeleted", nIcon_Id);
            }
        }
        public async Task GetAllNIcons()
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveAllNIcons", _nIcons);
        }
        public async Task GetByIdNIcon(int nIcon_Id)
        {
            if (Clients is not null)
            {
                var nicon = _nIcons.Find(ni => ni.NIcon_Id == nIcon_Id);
                await Clients.Caller.SendAsync("ReceiveNIcon", nicon);
            }

        }
        public async Task UpdateNIcon(NIcon updatedNIcon)
        {
            var nicon = _nIcons.Find(ni => ni.NIcon_Id == updatedNIcon.NIcon_Id);
            if (Clients is not null)
            {
                nicon.NIconName = updatedNIcon.NIconName;
                nicon.NIconDescription = updatedNIcon.NIconDescription;
                nicon.NIconUrl = updatedNIcon.NIconUrl;
                await Clients.All.SendAsync("ReceiveNIcon", nicon);
            }
        }
    }
}
