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

        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("receivemessage", message);
        }
        public async Task JoinGroup(string groupName, string pseudo)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await SendToGroup(new Message
            {
                Author = "System",
                NewMessage = "A new user has logged in" + pseudo,
                //Evenement_Id = Message.Evenement_Id,
            }, groupName);
        }
        public async Task SendToGroup(Message message, string groupName)
        {
            await Clients.Group(groupName).SendAsync("messagefromgroup", message);
        }
        public async Task RefreshChat()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewchat");
            }
        }
    }
}
