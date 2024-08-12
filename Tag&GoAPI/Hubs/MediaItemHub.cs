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

        public MediaItemHub()
        {
        }

        public async Task SubmitMediaItem(MediaItem mediaItem)
        {
            if (Clients is not null)
            {
                _mediaItems.Add(mediaItem);
                await Clients.All.SendAsync("receiveMediaitem", mediaItem);
            }
        }
        public async Task RefreshMediaItem()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewmediaitem");
            }
        }
        public async Task DeleteMediaItem(int mediaItem_Id)
        {
            var mediaItem = _mediaItems.Find(mi => mi.MediaItem_Id == mediaItem_Id);
            if (Clients is not null)
            {
                _mediaItems.Remove(mediaItem);
                await Clients.All.SendAsync("mediaItemDeleted", mediaItem_Id);
            }
        }
        public async Task submit()
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveAllMediaItem", _mediaItems);
        }
        public async Task GetByIdMediaItem(int mediaItem_Id)
        {
            var mediaItem = _mediaItems.Find(mi => mi.MediaItem_Id == mediaItem_Id);
            if (Clients is not null)

                await Clients.Caller.SendAsync("ReceiveMediaitem", mediaItem);
        }
        public async Task getAllMediaItems()
        {
            if (Clients is not null)
                await Clients.All.SendAsync("ReceiveAllMediaItems", _mediaItems);
        }
        public async Task updateMediaItem(MediaItem updateMediaItem)
        {
            var mediaitem = _mediaItems.Find(me => me.MediaItem_Id == updateMediaItem.MediaItem_Id);
            if (Clients is not null)
            {
                mediaitem.MediaType = updateMediaItem.MediaType;
                mediaitem.UrlItem = updateMediaItem.UrlItem;
                mediaitem.Description = updateMediaItem.Description;
                await Clients.All.SendAsync("ReceiveMediaItem", mediaitem);
            }
        }
    }
}
