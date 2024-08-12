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
    public class BonusController : ControllerBase
    {
    #nullable disable
        private readonly IBonusRepository _bonusRepository;
        private readonly BonusHub _bonusHub;
        private readonly Dictionary<string, string> _currentBonus = new Dictionary<string, string>();

        public BonusController(IBonusRepository bonusRepository, BonusHub bonusHub)
        {
            _bonusRepository = bonusRepository;
            _bonusHub = bonusHub;
        }
        [HttpGet]
        public IActionResult GetAllBonuss()
        {
            return Ok(_bonusRepository.GetAllBonuss());
        }
        [HttpGet("{bonus_Id}")]
        public IActionResult GetById(int bonus_Id)
        {
            try
            {
                var bonus = _bonusRepository.GetByIdBonus(bonus_Id);
                if (!ModelState.IsValid) 
                {
                    return NotFound();
                }
                return Ok(_bonusRepository.GetByIdBonus(bonus_Id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(BonusRegisterForm bonus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                if (_bonusRepository.Create(bonus.BonusToDal()))
                {
                    await _bonusHub.RefreshBonus();
                    return Ok();
                }
                return BadRequest("Registration Error");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating bonus: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpDelete("{bonus_Id}")]
        public IActionResult DeleteBonus(int bonus_Id)
        {
            _bonusRepository.DeleteBonus(bonus_Id);
            return Ok();
        }
        [HttpPut("{bonus_Id}")]
        public IActionResult UpdateBonus(int bonus_Id, string bonusType, string bonusDescription, string application, string granted)
        {
            _bonusRepository.UpdateBonus(bonus_Id, bonusType, bonusDescription, application, granted);
            return Ok();
        }
        [HttpPost("update")]
        public IActionResult ReceiveBonusUpdate(Dictionary<string, string> newUpdate)
        {
            foreach (var item in newUpdate)
            {
                _currentBonus[item.Key] = item.Value;
            }
            return Ok(_currentBonus);
        }
    }
}
