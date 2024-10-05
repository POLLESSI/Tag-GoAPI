using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.ChatActivity;

namespace Tag_Go.BLL.Interfaces
{
    public interface IChatActivityService
    {
    #nullable disable
        bool CreateChatActivity(ChatActivity chat);
        void CreateChat(ChatActivity chat);
        Task<IEnumerable<ChatActivity?>> GetAllMessagesActivities();
        Task<ChatActivity?> GetByIdChatActivity(int chat_Id);
        Task<ChatActivity?> DeleteMessageActivity(int chat_Id);
    }
}
