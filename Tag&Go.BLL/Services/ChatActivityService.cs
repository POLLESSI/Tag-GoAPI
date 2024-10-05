using Tag_Go.BLL.Interfaces;
using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Interfaces;
using Tag_Go.DAL.Repositories;
using Tag_Go.BLL;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;

namespace Tag_Go.BLL.Services
{
    public class ChatActivityService : IChatActivityService
    {
    #nullable disable
        private readonly IChatActivityRepository _chatActivityRepository;

        public ChatActivityService(IChatActivityRepository chatActivityRepository)
        {
            _chatActivityRepository = chatActivityRepository;
        }

        public bool CreateChatActivity(ChatActivity chatActivity)
        {
            try
            {
                return _chatActivityRepository.CreateChatActivity(chatActivity);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating chat : {ex.ToString}");
            }
            return false;
        }

        public void CreateChat(ChatActivity chat)
        {
            try
            {
                _chatActivityRepository.CreateChat(chat);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateChat : {ex.ToString}");
            }
        }

        public Task<ChatActivity?> DeleteMessageActivity(int chatActivity_Id)
        {
            try
            {
                return _chatActivityRepository.DeleteMessageActivity(chatActivity_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting chat : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<ChatActivity?>> GetAllMessagesActivities()
        {
            return _chatActivityRepository.GetAllMessagesActivities();
        }

        public Task<ChatActivity?> GetByIdChatActivity(int chat_Id)
        {
            try
            {
                return _chatActivityRepository.GetByIdChatActivity(chat_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting chat: {ex.ToString}");
            }
            return null;
        }
    }
}
