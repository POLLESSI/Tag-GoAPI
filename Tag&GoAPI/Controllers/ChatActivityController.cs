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
        public async Task<IActionResult> GetAllMessages()
        {
            try
            {
                var chat = await _chatActivityRepository.GetAllMessages();
                return Ok(chat);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("{chat_Id}")]
        public async Task<IActionResult> GetByIdChat(int chat_Id)
        {
            try
            {
                var chat = await _chatActivityRepository.GetByIdChat(chat_Id);
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
        [HttpPost]
        public async Task<IActionResult> Create(MessageActivityModel newMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var chatActivityDal = newMessage.ChatActivityToDal();
                var chatCreated = _chatActivityRepository.Create(chatActivityDal);

                if (chatCreated)
                {
                    await _chatActivityHub.RefreshChatActivity();

                    return CreatedAtAction(nameof(Create), new { id = chatActivityDal.Chat_Id}, chatActivityDal);
                }
                return BadRequest(new { message = "Registration error. Could not create chat" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating chat: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpDelete("{chat_Id}")]
        public async Task<IActionResult> DeleteMessage(int chat_Id)
        {
            try
            {
                var message = await _chatActivityRepository.DeleteMessage(chat_Id);
                if (!ModelState.IsValid)
                {
                    await _chatActivityRepository.DeleteMessage(chat_Id);
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
