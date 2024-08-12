using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_GoAPI.Hubs;
using Tag_Go.DAL.Interfaces;
using Tag_GoAPI.DTOs.Forms;
using Tag_GoAPI.Tools;
using System.Security.Cryptography;
//using System.Reflection.Metadata.Ecma335;

namespace Tag_GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NVoteController : ControllerBase
    {
    #nullable disable
        private readonly INVoteRepository _nVoteRepository;
        private readonly NVoteHub _nVoteHub;
        private readonly Dictionary<string, NVoteHub> _currentNVote = new Dictionary<string, NVoteHub>();

        public NVoteController(INVoteRepository nVoteRepository, NVoteHub nVoteHub)
        {
            _nVoteRepository = nVoteRepository;
            _nVoteHub = nVoteHub;
        }
        [HttpGet]
        public IActionResult GetAllNVotes()
        {
            return Ok(_nVoteRepository.GetAllNVotes());
        }
        [HttpGet("{nVote_Id}")]
        public IActionResult GetByIdNVote(int nVote_Id) 
        {
            try
            {
                var nvote = _nVoteRepository.GetByIdNVote(nVote_Id);
                if (!ModelState.IsValid) 
                {
                    return NotFound();
                }
                return Ok(_nVoteRepository.GetByIdNVote(nVote_Id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(NVoteRegisterForm nVote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                if (_nVoteRepository.Create(nVote.NVoteToDal()))
                {
                    await _nVoteHub.RefreshVote();
                    return Ok();
                }
                return BadRequest("Registration Error");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating vote: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpDelete("{nVote_Id}")]
        public IActionResult DeleteNVote(int nVote_Id)
        {
            _nVoteRepository.DeleteNVote(nVote_Id);
            return Ok();
        }
        [HttpPost("update")]
        public IActionResult ReceiveVoteUpdate(Dictionary<string, NVoteHub> newUpdate)
        {
            foreach (var item in newUpdate)
            {
                _currentNVote[item.Key] = item.Value;
            }
            return Ok(_currentNVote);
        }
    }
}
