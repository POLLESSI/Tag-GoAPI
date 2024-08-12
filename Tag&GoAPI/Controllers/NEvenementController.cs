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
        private readonly Dictionary<string, NEvenementHub> _currentNEvenement = new Dictionary<string, NEvenementHub>();

        public NEvenementController(INEvenementRepository nEvenementRepository, NEvenementHub nEvenementHub)
        {
            _nEvenementRepository = nEvenementRepository;
            _nEvenementHub = nEvenementHub;
        }
        [HttpGet]
        public IActionResult GetAllNEvenements()
        {
            return Ok(_nEvenementRepository.GetAllNEvenements());
        }
        [HttpGet("{nEvenement_Id}")]
        public IActionResult GetByIdNEvenement(int nEvenement_Id)
        {
            try
            {
                var nevenement = _nEvenementRepository.GetByIdNEvenement(nEvenement_Id);
                if (!ModelState.IsValid) 
                {
                    return NotFound();
                }
                return Ok(_nEvenementRepository.GetByIdNEvenement(nEvenement_Id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(NEvenementRegisterForm nEvenement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                if (_nEvenementRepository.Create(nEvenement.NEvenementToDal()))
                {
                    await _nEvenementHub.RefreshEvenement();
                    return Ok();
                }
                return BadRequest("Registration Error");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating event: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpDelete("{nEvenement_Id}")]
        public IActionResult DeleteNEvenement(int nEvenement_Id)
        {
            _nEvenementRepository.DeleteNEvenement(nEvenement_Id);
            return Ok();
        }
        [HttpPut("{nEvenement_Id}")]
        public IActionResult UpdateNEvenement(DateTime nEvenementDate, string nEvenementName, string nEvenementDescription, string posLat, string posLong, string positif, int organisateur_Id, int nIcon_Id, int recompense_Id, int bonus_Id, int mediaItem_Id, int nEvenement_Id)
        {
            _nEvenementRepository.UpdateNEvenement(nEvenementDate, nEvenementDescription, posLat, posLong, positif, organisateur_Id, nIcon_Id, recompense_Id, bonus_Id, mediaItem_Id, nEvenement_Id);
            return Ok();
        }
        [HttpPost("update")]
        public IActionResult ReceiveEvenementUpdate(Dictionary<string, NEvenementHub> newUpdate)
        {
            foreach (var item in newUpdate)
            {
                _currentNEvenement[item.Key] = item.Value;
            }
            return Ok(_currentNEvenement);
        }
    }
}
