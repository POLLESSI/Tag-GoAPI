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
        
        public NVoteController(INVoteRepository nVoteRepository, NVoteHub nVoteHub)
        {
            _nVoteRepository = nVoteRepository;
            _nVoteHub = nVoteHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNVotes()
        {
            try
            {
                var nvotes = await _nVoteRepository.GetAllNVotes();
                return Ok(nvotes);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{nVote_Id}")]
        public async Task<IActionResult> GetByIdNVote(int nVote_Id)
        {
            try
            {
                var nvote = await _nVoteRepository.GetByIdNVote(nVote_Id);
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
                return BadRequest(ModelState);
            }
            try
            {
                var nVoteDal = nVote.NVoteToDal();
                var nVoteCreated = _nVoteRepository.Create(nVoteDal);

                if (nVoteCreated)
                {
                    await _nVoteHub.RefreshVote();

                    return CreatedAtAction(nameof(Create), new { id = nVoteDal.NVote_Id }, nVoteDal);
                }
                return BadRequest(new { message = "Registration Error. Could not create new vote" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating vote: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        [HttpDelete("{nVote_Id}")]
        public async Task<IActionResult> DeleteNVote(int nVote_Id)
        {
            try
            {
                var nvote = await _nVoteRepository.DeleteNVote(nVote_Id);
                if (nvote == null)
                {
                    return NotFound($"Vote with ID {nVote_Id} not found");

                }
                return Ok("vote deleted successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error {ex.Message}");
            }

        }
    }
}
