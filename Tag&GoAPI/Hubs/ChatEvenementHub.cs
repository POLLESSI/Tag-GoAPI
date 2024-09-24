using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using Tag_Go.DAL.Entities;
using Tag_GoAPI.Models;

namespace Tag_GoAPI.Hubs
{
    public class ChatEvenementHub : Hub
    {
#nullable disable
        private static List<ChatEvenement> _chats = new List<ChatEvenement>();
        public async Task RefreshChatEvenement()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewchatevenement");
            }
        }
    }
}
