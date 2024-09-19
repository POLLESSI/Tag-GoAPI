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
                var avatars = await _avatarRepository.GetAllAvatars();
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
                return BadRequest(ModelState);
            }
            try
            {
                var avatarDal = avatar.AvatarToDal();
                var avatarCreated = _avatarRepository.Create(avatarDal);

                if (avatarCreated)
                {
                    await _avatarHub.RefreshAvatar();
                    return CreatedAtAction(nameof(Create), new { avatar_id = avatarDal.Avatar_Id}, avatarDal);
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
                if (!ModelState.IsValid)
                {
                    await _avatarRepository.DeleteAvatar(avatar_Id);
                }
                return Ok("Deleted");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAvatar(AvatarUpdate avatarUpdate)
        {
            var avatarDal = avatarUpdate.AvatarUpdateToDal();
            try
            {
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
