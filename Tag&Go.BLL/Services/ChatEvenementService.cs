using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag_Go.BLL.Interfaces;
using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Interfaces;
using Tag_Go.DAL.Repositories;
using Tag_Go.BLL;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;

namespace Tag_Go.BLL.Services
{
    public class ChatEvenementService : IChatEvenementService
    {
#nullable disable
        private readonly IChatEvenementRepository _chatEvenementRepository;

        public ChatEvenementService(IChatEvenementRepository chatEvenementRepository)
        {
            _chatEvenementRepository = chatEvenementRepository;
        }

        public async Task<ChatEvenement> CreateChatEvenement(ChatEvenement chat)
        {
            try
            {
                return await _chatEvenementRepository.CreateChatEvenement(chat);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating chat : {ex.ToString}");
                return null;
            }
            
        }

        public void CreateChat(ChatEvenement chat)
        {
            try
            {
                _chatEvenementRepository.CreateChat(chat);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateChat : {ex.ToString}");
            }
        }

        public Task<ChatEvenement?> DeleteMessageEvenement(int chat_Id)
        {
            try
            {
                return _chatEvenementRepository.DeleteMessageEvenement(chat_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting chat : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<ChatEvenement?>> GetAllMessagesEvenements(bool includeInactive = false)
        {
            try
            {
                return _chatEvenementRepository.GetAllMessagesEvenements();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error return Messages Evenements : {ex.Message}");
                return null;
            }
            
        }

        public Task<ChatEvenement?> GetByIdChatEvenement(int chat_Id)
        {
            try
            {
                return _chatEvenementRepository.GetByIdChatEvenement(chat_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting chat: {ex.ToString}");
            }
            return null;
        }
    }
}
