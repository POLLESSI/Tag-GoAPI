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
    public class OrganisateurController : ControllerBase
    {
    #nullable disable
        private readonly IOrganisateurRepository _organisateurRepository;
        private readonly OrganisateurHub _organisateurHub;
        
        public OrganisateurController(IOrganisateurRepository organisateurRepository, OrganisateurHub organisateurHub)
        {
            _organisateurRepository = organisateurRepository;
            _organisateurHub = organisateurHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrganisateurs()
        {
            try
            {
                bool isAdmin = User.IsInRole("Admin");
                var organisateurs = await _organisateurRepository.GetAllOrganisateurs(isAdmin);

                if (!organisateurs.Any()) 
                {
                    return NotFound("No active Organisators found.");
                }

                return Ok(organisateurs);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpGet("{organisateur_id}")]
        public IActionResult GetByIdOrganisateur(int organisateur_Id)
        {
            try
            {
                var organisateur = _organisateurRepository.GetByIdOrganisateur(organisateur_Id);
                if (organisateur == null)
                {
                    return NotFound($"Organisateur with ID {organisateur_Id} not found");
                }
                return Ok(organisateur);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving Organisateur: {ex.Message}");
            }

        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(OrganisateurRegisterForm newOrganisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var organisateurDal = newOrganisateur.OrganisateurToDal();
                Tag_Go.DAL.Entities.Organisateur organisateurCreated = await _organisateurRepository.Create(organisateurDal);

                if (organisateurCreated != null)
                {
                    await _organisateurHub.RefreshOrganisateur();

                    return CreatedAtAction(nameof(Create), new { id = organisateurCreated.Organisateur_Id }, organisateurCreated);
                }
                return BadRequest(new { message = "Registration Error. Could not create organisator" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating organisator: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        [HttpDelete("{organisateur_id}")]
        public async Task<IActionResult> DeleteOrganisateur(int organisateur_Id)
        {
            try
            {
                var organisateur = await _organisateurRepository.DeleteOrganisateur(organisateur_Id);
                if (organisateur == null)
                {
                    return NotFound($"Organisateur with ID {organisateur_Id} not found");
                }
                return Ok("Organisateur deleted successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error {ex.Message}");
            }

        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrganisateur(OrganisateurUpdate organisateurUpdate)
        {
            try
            {
                var organisateurDal = organisateurUpdate.OrganisateurUpdateToDal();
                var updateOrganisateur = await _organisateurRepository.UpdateOrganisateur(organisateurDal);

                if (updateOrganisateur == null)
                {
                    return NotFound($"Organisator with ID {organisateurDal.Organisateur_Id} not found.");
                }

                return Ok(updateOrganisateur);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }


        }
    }
}
