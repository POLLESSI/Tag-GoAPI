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
        public IActionResult GetAllMessages()
        {
            return Ok(_chatRepository.GetAllMessages());
        }
        [HttpGet("{chat_Id}")]
        public IActionResult GetByIdChat(int chat_Id)
        {
            try
            {
                var chat = _chatRepository.GetByIdChat(chat_Id);
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
        public IActionResult DeleteMessage(int chat_Id)
        {
            _chatRepository.DeleteMessage(chat_Id);
            return Ok();
        }
    }
}
