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

        public bool Create(ChatEvenement chat)
        {
            try
            {
                return _chatEvenementRepository.Create(chat);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating chat : {ex.ToString}");
            }
            return false;
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

        public Task<ChatEvenement?> DeleteMessage(int chat_Id)
        {
            try
            {
                return _chatEvenementRepository.DeleteMessage(chat_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting chat : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<ChatEvenement?>> GetAllMessages()
        {
            return _chatEvenementRepository.GetAllMessages();
        }

        public Task<ChatEvenement?> GetByIdChat(int chat_Id)
        {
            try
            {
                return _chatEvenementRepository.GetByIdChat(chat_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting chat: {ex.ToString}");
            }
            return null;
        }
    }
}
