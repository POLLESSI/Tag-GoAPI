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
        //[HttpGet]
        //public async Task <IActionResult> GetAllAvatars()
        //{
        //    try
        //    {
        //        var avatars = await _avatarRepository.GetAllAvatars();
        //        return Ok(avatars);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);

        //    }
            
        //}
        //[HttpGet("{avatar_Id}")]
        //public async Task<IActionResult> GetByIdAvatar(int avatar_Id)
        //{
        //    try
        //    {
        //        var avatar = await _avatarRepository.GetByIdAvatar(avatar_Id);
        //        if (!ModelState.IsValid) 
        //        {
        //            return NotFound();
        //        }
        //        return Ok(_avatarRepository.GetByIdAvatar(avatar_Id));
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(StatusCodes.Status400BadRequest, ex.Message); 
        //    }
            
        //}
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
        //[HttpDelete("{avatar_Id}")]
        //public async Task<IActionResult> DeleteAvatar(int avatar_Id)
        //{
        //    try
        //    {
        //        var avatar = await _avatarRepository.DeleteAvatar(avatar_Id);
        //        if (!ModelState.IsValid)
        //        {
        //            await _avatarRepository.DeleteAvatar(avatar_Id);
        //        }
        //        return Ok("Deleted");
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }
           
        //}
        //[HttpPut("{avatar_Id}")]
        //public async Task<IActionResult> UpdateAvatar(int avatar_Id, string avatarName, string avatarUrl, string description)
        //{
        //    try
        //    {
        //        var avatar = await _avatarRepository.UpdateAvatar(avatar_Id, avatarName, avatarUrl, description);
                
        //        return Ok("Updated");
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }
            
        //}
        //[HttpPost("update")]
        //public async Task<IActionResult> ReceiveAvatarUpdate(Dictionary<string, AvatarHub> newUpdate)
        //{
        //    foreach (var item in newUpdate)
        //    {
        //        try
        //        {
        //            _currentAvatar[item.Key] = item.Value;
        //        }
        //        catch (Exception ex)
        //        {

        //            BadRequest(ex.Message);
        //        }
                
        //    }
        //    return Ok(_currentAvatar);
        //}
    }
}
