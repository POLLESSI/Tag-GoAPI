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
        IEnumerable<Chat?> GetAllMessages();
        Chat? GetByIdChat(int chat_Id);
        Chat? DeleteMessage(int chat_Id);
    }
}
