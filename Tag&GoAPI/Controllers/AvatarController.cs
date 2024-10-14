using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_GoAPI.Hubs;
using Tag_Go.DAL.Interfaces;
using Tag_GoAPI.DTOs.Forms;
using Tag_GoAPI.Tools;
using System.Security.Cryptography;

namespace Tag_GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvatarController : ControllerBase
    {
    #nullable disable
        private readonly IAvatarRepository _avatarRepository;
        private readonly AvatarHub _avatarHub;

        public AvatarController(IAvatarRepository avatarRepository, AvatarHub avatarHub)
        {
            _avatarRepository = avatarRepository;
            _avatarHub = avatarHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAvatars()
        {
            try
            {
                bool isAdmin = User.IsInRole("Admin");
                var avatars = await _avatarRepository.GetAllAvatars(isAdmin);
                if (!avatars.Any()) 
                {
                    return NotFound("No active avatars found.");
                }
                return Ok(avatars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
        [HttpGet("{avatar_Id}")]
        public async Task<IActionResult> GetByIdAvatar(int avatar_Id)
        {
            try
            {
                var avatar = await _avatarRepository.GetByIdAvatar(avatar_Id);
                if (avatar == null)
                {
                    return NotFound($"Avatar with ID {avatar_Id} not found");
                }
                return Ok(avatar);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving avatar: {ex.Message}");
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(AvatarRegisterForm avatar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var avatarDal = avatar.AvatarToDal();
                Tag_Go.DAL.Entities.Avatar avatarCreated = await _avatarRepository.Create(avatarDal);

                if (avatarCreated != null)
                {
                    await _avatarHub.RefreshAvatar();
                    return CreatedAtAction(nameof(Create), new { avatar_id = avatarCreated.Avatar_Id}, avatarCreated);
                }
                return BadRequest(new { message = "Registration Error. Could not create avatar" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating avatar: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        [HttpDelete("{avatar_Id}")]
        public async Task<IActionResult> DeleteAvatar(int avatar_Id)
        {
            try
            {
                var avatar = await _avatarRepository.DeleteAvatar(avatar_Id);
                if (avatar == null)
                {
                    return NotFound($"Avatar with ID {avatar_Id} not found");
                }
                return Ok("Avatar deleted successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error {ex.Message}");
            }

        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAvatar(AvatarUpdate avatarUpdate)
        {
            
            try
            {
                var avatarDal = avatarUpdate.AvatarUpdateToDal();
                var updateAvatar = await _avatarRepository.UpdateAvatar(avatarDal);

                if (updateAvatar == null)
                {
                    return NotFound($"Avatar with ID {avatarDal.Avatar_Id} not found.");
                }

                return Ok(updateAvatar);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }
}
