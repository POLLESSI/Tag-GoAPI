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
        private readonly Dictionary<string, OrganisateurHub> _currentOrganisateur = new Dictionary<string, OrganisateurHub>();

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
                var organisateurs = await _organisateurRepository.GetAllOrganisateurs();
                return Ok(organisateurs);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        //[HttpGet("{organisateur_id}")]
        //public IActionResult GetByIdOrganisateur(int organisateur_Id)
        //{
        //    try
        //    {
        //        var organisateur = _organisateurRepository.GetByIdOrganisateur(organisateur_Id);
        //        if (!ModelState.IsValid) 
        //        {
        //            return NotFound();
        //        }
        //        return Ok(_organisateurRepository.GetByIdOrganisateur(organisateur_Id));
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
        //    }

        //}
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
                var organisateurCreated = _organisateurRepository.Create(organisateurDal);

                if (organisateurCreated)
                {
                    await _organisateurHub.RefreshOrganisateur();

                    return CreatedAtAction(nameof(Create), new { id = organisateurDal.Organisateur_Id });
                }
                return BadRequest(new { message = "Registration Error. Could not create organisator" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating organisator: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        //[HttpDelete("{organisateur_id}")]
        //public async Task<IActionResult> DeleteOrganisateur(int organisateur_Id)
        //{
        //    try
        //    {
        //        var organisateur = await _organisateurRepository.DeleteOrganisateur(organisateur_Id);
        //        if (!ModelState.IsValid)
        //        {
        //            return NotFound();
        //        }
        //        return Ok("Deleted");
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }

        //}
        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrganisateur(OrganisateurUpdate organisateurUpdate)
        {
            var organisateurDal = organisateurUpdate.OrganisateurUpdateToDal();
            try
            {
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
        //[HttpPost("update")]
        //public async Task<IActionResult> ReceiveOrganisateurUpdate(Dictionary<string, OrganisateurHub> newOrganisateur)
        //{
        //    foreach (var item in newOrganisateur)
        //    {
        //        try
        //        {
        //            _currentOrganisateur[item.Key] = item.Value;
        //        }
        //        catch (Exception ex)
        //        {

        //            BadRequest(ex.Message);
        //        }

        //    }
        //    return Ok(_currentOrganisateur);
        //}
    }
}
