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
    public class NEvenementController : ControllerBase
    {
    #nullable disable
        private readonly INEvenementRepository _nEvenementRepository;
        private readonly NEvenementHub _nEvenementHub;
        
        public NEvenementController(INEvenementRepository nEvenementRepository, NEvenementHub nEvenementHub)
        {
            _nEvenementRepository = nEvenementRepository;
            _nEvenementHub = nEvenementHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllNEvenements()
        {
            try
            {
                bool isAdmin = User.IsInRole("Admin");
                var nevenements = await _nEvenementRepository.GetAllNEvenements(isAdmin);

                if (!nevenements.Any()) 
                {
                    return NotFound("No active events found.");
                }

                return Ok(nevenements);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpGet("{nEvenement_Id}")]
        public async Task<IActionResult> GetByIdNEvenement(int nEvenement_Id)
        {
            try
            {
                var nevenement = await _nEvenementRepository.GetByIdNEvenement(nEvenement_Id);
                if (nevenement == null)
                {
                    return NotFound($"Event with ID {nEvenement_Id} don't found");
                }
                return Ok(nevenement);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving Event: {ex.Message}");
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(NEvenementRegisterForm nEvenement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nEvenementDal = nEvenement.NEvenementToDal();
                Tag_Go.DAL.Entities.NEvenement nEvenementCreated = await _nEvenementRepository.Create(nEvenementDal);

                if (nEvenementCreated != null)
                {
                    await _nEvenementHub.RefreshEvenement();

                    return CreatedAtAction(nameof(Create), new { id = nEvenementCreated.NEvenement_Id }, nEvenementCreated);
                }
                return BadRequest(new { message = "Registration Error. Could not create event" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating event: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        [HttpDelete("{nEvenement_Id}")]
        public async Task<IActionResult> DeleteNEvenement(int nEvenement_Id)
        {
            try
            {
                var nevenement = await _nEvenementRepository.DeleteNEvenement(nEvenement_Id);
                if (nevenement == null)
                {
                    return NotFound($"Event with ID {nEvenement_Id} not found");
                }
                return Ok("Event deleted successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error {ex.Message}");
            }

        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateNEvenement(NEvenementUpdate nEvenementUpdate)
        {
            try
            {
                var nEvenementDal = nEvenementUpdate.NEvenementUpdateToDal();
                var updatedNEvenment = await _nEvenementRepository.UpdateNEvenement(nEvenementDal);

                if (updatedNEvenment == null)
                {
                    return NotFound($"Event with ID {nEvenementDal.NEvenement_Id} not found");
                }

                return Ok(updatedNEvenment);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }
}
