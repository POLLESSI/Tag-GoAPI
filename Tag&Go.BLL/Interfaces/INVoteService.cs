using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.NVote;

namespace Tag_Go.BLL.Interfaces
{
    public interface INVoteService
    {
    #nullable disable
        bool Create(NVote nVote);
        void CreateVote(NVote nVote);
        Task<IEnumerable<NVote?>> GetAllNVotes();
        Task<NVote?> GetByIdNVote(int nVote_Id);
        Task<NVote?> DeleteNVote(int nVote_Id);
    }
}
