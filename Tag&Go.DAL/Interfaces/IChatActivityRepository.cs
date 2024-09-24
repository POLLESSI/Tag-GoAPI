﻿using Tag_Go.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.ChatActivity;

namespace Tag_Go.DAL.Interfaces
{
    public interface IChatActivityRepository
    {
    #nullable disable
        bool Create(ChatActivity chat);
        void CreateChat(ChatActivity chat);
        Task<IEnumerable<ChatActivity?>> GetAllMessages();
        Task<ChatActivity?> GetByIdChat(int chat_Id);
        Task<ChatActivity?> DeleteMessage(int chat_Id);
    }
}
