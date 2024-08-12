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
    public class AvatarController : ControllerBase
    {
    #nullable disable
        private readonly IAvatarRepository _avatarRepository;
        private readonly AvatarHub _avatarHub;
        private Dictionary<string, AvatarHub> _currentAvatar = new Dictionary<string, AvatarHub>();

        public AvatarController(IAvatarRepository avatarRepository, AvatarHub avatarHub)
        {
            _avatarRepository = avatarRepository;
            _avatarHub = avatarHub;
        }
        [HttpGet]
        public IActionResult GetAllAvatars()
        {
            return Ok(_avatarRepository.GetAllAvatars());
        }
        [HttpGet("{avatar_Id}")]
        public IActionResult GetByIdAvatar(int avatar_Id)
        {
            try
            {
                var avatar = _avatarRepository.GetByIdAvatar(avatar_Id);
                if (!ModelState.IsValid) 
                {
                    return NotFound();
                }
                return Ok(_avatarRepository.GetByIdAvatar(avatar_Id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest, ex.Message); 
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(AvatarRegisterForm avatar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                if (_avatarRepository.Create(avatar.AvatarToDal()))
                {
                    await _avatarHub.RefreshAvatar();
                    return Ok();
                }
                return BadRequest("Registration Error");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating avatar: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpDelete("{avatar_Id}")]
        public IActionResult DeleteAvatar(int avatar_Id)
        {
            _avatarRepository.DeleteAvatar(avatar_Id);
            return Ok();
        }
        [HttpPut("{avatar_Id}")]
        public IActionResult UpdateAvatar(int avatar_Id, string avatarName, string avatarUrl, string description)
        {
            _avatarRepository.UpdateAvatar(avatar_Id, avatarName, avatarUrl, description);
            return Ok();
        }
        [HttpPost("update")]
        public IActionResult ReceiveAvatarUpdate(Dictionary<string, AvatarHub> newUpdate)
        {
            foreach (var item in newUpdate)
            {
                _currentAvatar[item.Key] = item.Value;
            }
            return Ok(_currentAvatar);
        }
    }
}
