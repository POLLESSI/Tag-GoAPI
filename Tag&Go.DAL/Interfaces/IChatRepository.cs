using Tag_Go.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.Chat;

namespace Tag_Go.DAL.Interfaces
{
    public interface IChatRepository
    {
    #nullable disable
        bool Create(Chat chat);
        void CreateChat(Chat chat);
        Task<IEnumerable<Chat?>> GetAllMessages();
        Task<Chat?> GetByIdChat(int chat_Id);
        Task<Chat?> DeleteMessage(int chat_Id);
    }
}
