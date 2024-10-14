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
    public class ChatActivityController : ControllerBase
    {
    #nullable disable
        private readonly IChatActivityRepository _chatActivityRepository;
        private readonly ChatActivityHub _chatActivityHub;

        public ChatActivityController(IChatActivityRepository chatActivityRepository, ChatActivityHub chatActivityHub)
        {
            _chatActivityRepository = chatActivityRepository;
            _chatActivityHub = chatActivityHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMessagesActivities()
        {
            try
            {
                bool isAdmin = User.IsInRole("Admin");
                var chat = await _chatActivityRepository.GetAllMessagesActivities(isAdmin);
                if (!chat.Any())
                {
                    return NotFound("No active activities found.");
                }
                return Ok(chat);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}" );
            }

        }
        [HttpGet("{chat_Id}")]
        public async Task<IActionResult> GetByIdChatActivity(int chat_Id)
        {
            try
            {
                var chat = await _chatActivityRepository.GetByIdChatActivity(chat_Id);
                if (chat == null)
                {
                    return NotFound($"Chat Activity with ID {chat_Id} not found");
                }
                return Ok(chat);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving chat: {ex.Message}");
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateChatActivity(MessageActivityModel newMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var chatActivityDal = newMessage.ChatActivityToDal();
                Tag_Go.DAL.Entities.ChatActivity chatActivityCreated = await _chatActivityRepository.CreateChatActivity(chatActivityDal);

                if (chatActivityCreated != null)
                {
                    await _chatActivityHub.RefreshChatActivity();

                    return CreatedAtAction(nameof(CreateChatActivity), new { id = chatActivityCreated.ChatActivity_Id}, chatActivityCreated);
                }
                return BadRequest(new { message = "Registration error. Could not create chat Activity" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating chat Activity: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        [HttpDelete("{chatActivity_Id}")]
        public async Task<IActionResult> DeleteMessageActivity(int chatActivity_Id)
        {
            try
            {
                var message = await _chatActivityRepository.DeleteMessageActivity(chatActivity_Id);
                if (!ModelState.IsValid)
                {
                    await _chatActivityRepository.DeleteMessageActivity(chatActivity_Id);
                }
                return Ok("Deleted");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }
}
