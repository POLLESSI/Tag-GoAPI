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
        public IActionResult GetAllOrganisateurs()
        {
            return Ok(_organisateurRepository.GetAllOrganisateurs());
        }
        [HttpGet("{organisateur_id}")]
        public IActionResult GetByIdOrganisateur(int organisateur_Id)
        {
            try
            {
                var organisateur = _organisateurRepository.GetByIdOrganisateur(organisateur_Id);
                if (!ModelState.IsValid) 
                {
                    return NotFound();
                }
                return Ok(_organisateurRepository.GetByIdOrganisateur(organisateur_Id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(OrganisateurRegisterForm newOrganisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                if (_organisateurRepository.Create(newOrganisateur.OrganisateurToDal()))
                {
                    await _organisateurHub.RefreshOrganisateur();
                    return Ok(newOrganisateur);
                }
                return BadRequest("Registration Error");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating organisator: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpDelete("{organisateur_id}")]
        public IActionResult DeleteOrganisateur(int organisateur_Id)
        {
            _organisateurRepository.DeleteOrganisateur(organisateur_Id);
            return Ok();
        }
        [HttpPut("{organisateur_Id}")]
        public IActionResult UpdateOrganisateur(string companyName, string businessNumber, int nUser_Id, string point, int organisateur_Id)
        {
            _organisateurRepository.UpdateOrganisateur(companyName, businessNumber, nUser_Id, point, organisateur_Id);
            return Ok();
        }
        [HttpPost("update")]
        public IActionResult ReceiveOrganisateurUpdate(Dictionary<string, OrganisateurHub> newOrganisateur)
        {
            foreach (var item in newOrganisateur)
            {
                _currentOrganisateur[item.Key] = item.Value;
            }
            return Ok(_currentOrganisateur);
        }
    }
}
