using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_Go.DAL.Interfaces;
using System.Security.Cryptography;
using Tag_GoAPI.Hubs;
using Tag_GoAPI.Models;
using Tag_GoAPI.Tools;
//using System.Reflection.Metadata.Ecma335;

namespace Tag_GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
    #nullable disable
        private readonly IChatRepository _chatRepository;
        private readonly ChatHub _chatHub;

        public ChatController(IChatRepository chatRepository, ChatHub chatHub)
        {
            _chatRepository = chatRepository;
            _chatHub = chatHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMessages()
        {
            try
            {
                var chat = await _chatRepository.GetAllMessages();
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
                var chat = await _chatRepository.GetByIdChat(chat_Id);
                if (!ModelState.IsValid) 
                {
                    return NotFound();
                }
                return Ok(_chatRepository.GetByIdChat(chat_Id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(MessageModel newMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                if (_chatRepository.Create(newMessage.ChatToDal()))
                {
                    await _chatHub.RefreshChat();
                    return Ok(newMessage);
                }
                return BadRequest("Registration error");
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
                var message = await _chatRepository.DeleteMessage(chat_Id);
                if (!ModelState.IsValid)
                {
                    await _chatRepository.DeleteMessage(chat_Id);
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
