using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using Tag_Go.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tag_GoAPI.Hubs
{
    public class MediaItemHub : Hub
    {
    #nullable disable
        private static List<MediaItem> _mediaItems = new List<MediaItem>();
        public async Task RefreshMediaItem()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewmediaitem");
            }
        }
    }
}
