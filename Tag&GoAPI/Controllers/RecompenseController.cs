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
    public class RecompenseController : ControllerBase
    {
    #nullable disable
        private readonly IRecompenseRepository _recompenseRepository;
        private readonly RecompenseHub _recompenseHub;
        
        public RecompenseController(IRecompenseRepository recompenseRepository, RecompenseHub recompenseHub)
        {
            _recompenseRepository = recompenseRepository;
            _recompenseHub = recompenseHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRecompenses()
        {
            try
            {
                var recompenses = await _recompenseRepository.GetAllRecompenses();
                return Ok(recompenses);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("{recompense_Id}")]
        public async Task<IActionResult> GetByIdRecompense(int recompense_Id)
        {
            try
            {
                var recompense = await _recompenseRepository.GetByIdRecompense(recompense_Id);
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                return Ok(_recompenseRepository.GetByIdRecompense(recompense_Id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(RecompenseRegisterForm recompense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var recompenseDal = recompense.RecompenseToDal();
                var recompenseCreated = _recompenseRepository.Create(recompenseDal);

                if (recompenseCreated)
                {
                    await _recompenseHub.RefreshRecompense();

                    return CreatedAtAction(nameof(Create), new { id = recompenseDal.Recompense_Id}, recompenseDal);
                }
                return BadRequest(new { message = "Registration Error. Could not create recompense" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating recompense: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        [HttpDelete("{recompense_id}")]
        public async Task<IActionResult> DeleteRecompense(int recompense_Id)
        {
            try
            {
                var recompense = await _recompenseRepository.DeleteRecompense(recompense_Id);

                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                return Ok("Deleted");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateRecompense(RecompenseUpdate recompenseUpdate)
        {
            var recompenseDal = recompenseUpdate.RecompenseUpdateToDal();
            try
            {
                var updateRecompense = await _recompenseRepository.UpdateRecompense(recompenseDal);

                if (updateRecompense == null)
                {
                    return NotFound($"Recompense with ID {recompenseDal.Recompense_Id} not found");
                }

                return Ok(updateRecompense);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }
}
