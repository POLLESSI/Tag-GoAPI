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
        private readonly Dictionary<string, NIconHub> _currentNIcon = new Dictionary<string, NIconHub>();

        public NIconController(INIconRepository nIconRepository, NIconHub nIconHub)
        {
            _nIconRepository = nIconRepository;
            _nIconHub = nIconHub;
        }
        [HttpGet]
        public IActionResult GetAllNIcons()
        {
            return Ok(_nIconRepository.GetAllNIcons());
        }
        [HttpGet("{nIcon_Id}")]
        public IActionResult GetByIdNIcon(int nIconId)
        {
            try
            {
                var nicon = _nIconRepository.GetByIdNIcon(nIconId);
                if (!ModelState.IsValid) 
                {
                    return NotFound();
                }
                return Ok(_nIconRepository.GetByIdNIcon(nIconId));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(NIconRegisterForm nIcon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                if (_nIconRepository.Create(nIcon.NIconToDal()))
                {
                    await _nIconHub.RefreshIcon();
                    return Ok(nIcon);
                }
                return BadRequest("Registration Error");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating icon: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpDelete("{nIcon_Id}")]
        public IActionResult DeleteNIcon(int nIconId)
        {
            _nIconRepository.DeleteNIcon(nIconId);
            return Ok();
        }
        [HttpPut("{nIcon_Id}")]
        public IActionResult UpdateNIcon(string nIconName, string nIconDescription, string nIconUrl, int nIconId)
        {
            _nIconRepository.UpdateNIcon(nIconName, nIconDescription, nIconUrl, nIconId);
            return Ok();
        }
        [HttpPost("update")]
        public IActionResult ReceiveIconUpdate(Dictionary<string, NIconHub> newUpdate)
        {
            foreach (var item in newUpdate)
            {
                _currentNIcon[item.Key] = item.Value;
            }
            return Ok(_currentNIcon);
        }
    }
}
