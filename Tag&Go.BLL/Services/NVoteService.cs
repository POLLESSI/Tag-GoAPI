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
    public class NVoteService : INVoteService
    {
    #nullable disable
        private readonly INVoteRepository _nVoteRepository;

        public NVoteService(INVoteRepository nVoteRepository)
        {
            _nVoteRepository = nVoteRepository;
        }

        public bool Create(NVote nVote)
        {
            try
            {
                return _nVoteRepository.Create(nVote);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating new vote : {ex.ToString}");
            }
            return false;
        }

        public void CreateVote(NVote nVote)
        {
            try
            {
                _nVoteRepository.CreateVote(nVote);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateEvent : {ex.ToString}");
            }
        }

        public Task<NVote?> DeleteNVote(int nVote_Id)
        {
            try
            {
                return _nVoteRepository.DeleteNVote(nVote_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting vote : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<NVote?>> GetAllNVotes()
        {
            return _nVoteRepository.GetAllNVotes();
        }

        public Task<NVote?> GetByIdNVote(int nVote_Id)
        {
            try
            {
                return _nVoteRepository.GetByIdNVote(nVote_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting vote : {ex.ToString}");
            }
            return null;
        }
    }
}
