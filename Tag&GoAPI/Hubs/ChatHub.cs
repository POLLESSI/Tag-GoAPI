using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using Tag_Go.DAL.Entities;
using Tag_GoAPI.Models;

namespace Tag_GoAPI.Hubs
{
    public class ChatHub : Hub
    {
    #nullable disable
        private static List<Chat> _chats = new List<Chat>();
        public async Task RefreshChat()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewchat");
            }
        }
    }
}
