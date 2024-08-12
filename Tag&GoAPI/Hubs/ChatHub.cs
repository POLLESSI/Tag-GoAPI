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

        public ChatHub()
        {
        }

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
        public async Task SubmitChat(Chat chat)
        {
            if (chat is not null)
            {
                _chats.Add(chat);
                await Clients.All.SendAsync("ReceiveChat", chat);
            }
        }
        public async Task RefreshChat()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewchat");
            }
        }
        public async Task DeleteMessage(int chat_Id)
        {
            var chat = _chats.Find(ch => ch.Chat_Id == chat_Id);
            if (Clients is not null)
            {
                _chats.Remove(chat);
                await Clients.All.SendAsync("chatDeleted", chat_Id);
            }
        }
        public async Task GetAllMessages()
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveAllChat", _chats);
        }
        public async Task GetByIdChat(int chat_Id)
        {
            var chat = _chats.Find(ch => ch.Chat_Id == chat_Id);
            if (Clients is not null)
                await Clients.All.SendAsync("Receivechat", chat);
        }
    }
}
