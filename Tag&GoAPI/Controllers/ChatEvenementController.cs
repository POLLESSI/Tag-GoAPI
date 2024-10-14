using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_Go.DAL.Interfaces;
using System.Security.Cryptography;
using Tag_GoAPI.Hubs;
using Tag_GoAPI.Models;
using Tag_GoAPI.Tools;

namespace Tag_GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatEvenementController : ControllerBase
    {
#nullable disable
        private readonly IChatEvenementRepository _chatEvenementRepository;
        private readonly ChatEvenementHub _chatEvenementHub;

        public ChatEvenementController(IChatEvenementRepository chatEvenementRepository, ChatEvenementHub chatEvenementHub)
        {
            _chatEvenementRepository = chatEvenementRepository;
            _chatEvenementHub = chatEvenementHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMessagesEvenements()
        {
            try
            {
                bool isAdmin = User.IsInRole("Admin");
                var chatEvenement = await _chatEvenementRepository.GetAllMessagesEvenements(isAdmin);
                if (!chatEvenement.Any()) 
                {
                    return NotFound("No active chat event found.");
                }
                return Ok(chatEvenement);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("{chat_Id}")]
        public async Task<IActionResult> GetByIdChatEvenement(int chatEvenement_Id)
        {
            try
            {
                var chatEvenement = await _chatEvenementRepository.GetByIdChatEvenement(chatEvenement_Id);
                if (chatEvenement == null)
                {
                    return NotFound($"Chat With ID {chatEvenement_Id} not found");
                }
                return Ok(chatEvenement);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving chat event: {ex.Message}");
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateChatEvenement(MessageEvenementModel newMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var chatEvenementDal = newMessage.ChatEvenementToDal();
                Tag_Go.DAL.Entities.ChatEvenement chatCreatedEvenement = await _chatEvenementRepository.CreateChatEvenement(chatEvenementDal);

                if (chatCreatedEvenement != null)
                {
                    await _chatEvenementHub.RefreshChatEvenement();

                    return CreatedAtAction(nameof(CreateChatEvenement), new { id = chatCreatedEvenement.ChatEvenement_Id }, chatCreatedEvenement);
                }
                return BadRequest(new { message = "Registration error. Could not create chat" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating chat: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        [HttpDelete("{chat_Id}")]
        public async Task<IActionResult> DeleteMessageEvenement(int chatEvenement_Id)
        {
            try
            {
                var messageEvenement = await _chatEvenementRepository.DeleteMessageEvenement(chatEvenement_Id);
                if (!ModelState.IsValid)
                {
                    await _chatEvenementRepository.DeleteMessageEvenement(chatEvenement_Id);
                }
                return Ok("Deleted Event");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }
}
