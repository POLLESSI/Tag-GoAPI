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
    public class NIconController : ControllerBase
    {
    #nullable disable
        private readonly INIconRepository _nIconRepository;
        private readonly NIconHub _nIconHub;
        
        public NIconController(INIconRepository nIconRepository, NIconHub nIconHub)
        {
            _nIconRepository = nIconRepository;
            _nIconHub = nIconHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNIcons()
        {
            try
            {
                var nicons = await _nIconRepository.GetAllNIcons();
                return Ok(nicons);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("{nIcon_Id}")]
        public async Task<IActionResult> GetByIdNIcon(int nIcon_Id)
        {
            try
            {
                var nicon = await _nIconRepository.GetByIdNIcon(nIcon_Id);
                if (nicon == null)
                {
                    return NotFound($"Icon with ID {nIcon_Id}");
                }
                return Ok(nicon);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving Icon: {ex.Message}");
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(NIconRegisterForm nIcon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var iconDal = nIcon.NIconToDal();
                var iconCreated = _nIconRepository.Create(iconDal);

                if (iconCreated)
                {
                    await _nIconHub.RefreshIcon();

                    return CreatedAtAction(nameof(Create), new { id = iconDal.NIcon_Id }, iconDal);
                }
                return BadRequest(new { message = "Registration Error" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating icon: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        [HttpDelete("{nIcon_Id}")]
        public async Task<IActionResult> DeleteNIcon(int nIconId)
        {
            try
            {
                var nicon = await _nIconRepository.DeleteNIcon(nIconId);
                if (nicon == null)
                {
                    return NotFound($"Icon with ID {nIconId} not found");
                }
                return Ok("icon deleted successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error {ex.Message}");
            }

        }
        [HttpPut("{nIcon_Id}")]
        public async Task<IActionResult> UpdateNIcon(NIconUpdate nIconUpdate)
        {
            try
            {
                var nIconDal = nIconUpdate.NIconUpdateToDal();
                var updateNIcon = await _nIconRepository.UpdateNIcon(nIconDal);

                if (updateNIcon == null)
                {
                    return NotFound($"Icon with ID {nIconDal.NIcon_Id} not found.");
                }

                return Ok(updateNIcon);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }
}
