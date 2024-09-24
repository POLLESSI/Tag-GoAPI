using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;
using static Tag_Go.DAL.Entities.ChatEvenement;

namespace Tag_Go.DAL.Interfaces
{
    public interface IChatEvenementRepository
    {
#nullable disable
        bool Create(ChatEvenement chat);
        void CreateChat(ChatEvenement chat);
        Task<IEnumerable<ChatEvenement?>> GetAllMessages();
        Task<ChatEvenement?> GetByIdChat(int chat_Id);
        Task<ChatEvenement?> DeleteMessage(int chat_Id);
    }
}
