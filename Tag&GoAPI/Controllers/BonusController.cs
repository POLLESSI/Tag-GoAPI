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
        
        public BonusController(IBonusRepository bonusRepository, BonusHub bonusHub)
        {
            _bonusRepository = bonusRepository;
            _bonusHub = bonusHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBonuss()
        {
            try
            {
                bool isAdmin = User.IsInRole("admin");
                var bonus = await _bonusRepository.GetAllBonuss(isAdmin);

                if (!bonus.Any()) 
                {
                    return NotFound("No active bonus found.");
                }

                return Ok(bonus);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("{bonus_Id}")]
        public async Task<IActionResult> GetById(int bonus_Id)
        {
            try
            {
                var bonus = await _bonusRepository.GetByIdBonus(bonus_Id);
                if (bonus == null)
                {
                    return NotFound($"Bonus with ID {bonus_Id} not found");
                }
                return Ok(bonus);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving bonus: {ex.Message}");
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(BonusRegisterForm bonus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var bonusDal = bonus.BonusToDal();
                Tag_Go.DAL.Entities.Bonus bonusCreated = await _bonusRepository.Create(bonusDal);

                if (bonusCreated != null)
                {
                    await _bonusHub.RefreshBonus();

                    return CreatedAtAction(nameof(Create), new { id = bonusCreated.Bonus_Id}, bonusCreated);
                }
                return BadRequest(new { message = "Registration Error. Could not create activity" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating bonus: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        [HttpDelete("{bonus_Id}")]
        public async Task<IActionResult> DeleteBonus(int bonus_Id)
        {
            try
            {
                var bonus = await _bonusRepository.DeleteBonus(bonus_Id);
                if (bonus == null)
                {
                    return NotFound($"Bonus with ID {bonus_Id} not found");
                }

                return Ok("Bonus deleted successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error {ex.Message}");
            }

        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateBonus(BonusUpdate bonusUpdate)
        {
            

            try
            {
                var bonusDal = bonusUpdate.BonusUpdateToDal();
                var updatedBonus = await _bonusRepository.UpdateBonus(bonusDal);

                if (updatedBonus == null)
                {
                    return NotFound($"Bonus with ID {bonusDal.Bonus_Id} not found.");
                }
                return Ok(updatedBonus);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }
}
