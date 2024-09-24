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
        public async Task<IActionResult> GetAllMessages()
        {
            try
            {
                var chat = await _chatEvenementRepository.GetAllMessages();
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
                var chat = await _chatEvenementRepository.GetByIdChat(chat_Id);
                if (chat == null)
                {
                    return NotFound($"Chat With ID {chat_Id} not found");
                }
                return Ok(chat);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving chat event: {ex.Message}");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create(MessageEvenementModel newMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var chatEvenementDal = newMessage.ChatEvenementToDal();
                var chatCreated = _chatEvenementRepository.Create(chatEvenementDal);

                if (chatCreated)
                {
                    await _chatEvenementHub.RefreshChatEvenement();

                    return CreatedAtAction(nameof(Create), new { id = chatEvenementDal.Chat_Id }, chatEvenementDal);
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
                var message = await _chatEvenementRepository.DeleteMessage(chat_Id);
                if (!ModelState.IsValid)
                {
                    await _chatEvenementRepository.DeleteMessage(chat_Id);
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
