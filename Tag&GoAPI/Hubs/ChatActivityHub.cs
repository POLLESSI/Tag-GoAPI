using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using Tag_Go.DAL.Entities;
using Tag_GoAPI.Models;

namespace Tag_GoAPI.Hubs
{
    public class ChatActivityHub : Hub
    {
    #nullable disable
        private static List<ChatActivity> _chats = new List<ChatActivity>();
        public async Task RefreshChatActivity()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewchatactivity");
            }
        }
    }
}
