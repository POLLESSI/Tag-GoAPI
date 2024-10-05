using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;
using static Tag_Go.DAL.Entities.ChatEvenement;

namespace Tag_Go.BLL.Interfaces
{
    public interface IChatEvenementService
    {
#nullable disable
        bool CreateChatEvenement(ChatEvenement chat);
        void CreateChat(ChatEvenement chat);
        Task<IEnumerable<ChatEvenement?>> GetAllMessagesEvenements();
        Task<ChatEvenement?> GetByIdChatEvenement(int chat_Id);
        Task<ChatEvenement?> DeleteMessageEvenement(int chat_Id);
    }
}

