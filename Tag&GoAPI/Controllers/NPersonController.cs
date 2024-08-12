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
    public class NPersonController : ControllerBase
    {
    #nullable disable
        private readonly INPersonRepository _nPersonRepository;
        private readonly NPersonHub _nPersonHub;
        private readonly Dictionary<string, NPersonHub> _currentNPerson = new Dictionary<string, NPersonHub>();

        public NPersonController(INPersonRepository nPersonRepository, NPersonHub nPersonHub)
        {
            _nPersonRepository = nPersonRepository;
            _nPersonHub = nPersonHub;
        }
        [HttpGet]
        public IActionResult GetAllNPersons()
        {
            return Ok(_nPersonRepository.GetAllNPersons());
        }
        [HttpGet("{person_Id}")]
        public IActionResult GetByIdNPerson(int nPerson_Id)
        {
            try
            {
                var nperson = _nPersonRepository.GetByIdNPerson(nPerson_Id);
                if (!ModelState.IsValid) 
                {
                    return NotFound();
                }
                return Ok(_nPersonRepository.GetByIdNPerson(nPerson_Id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            
        }
        //[Authorize("AdminPolicy")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(NPersonRegisterForm nPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                if (_nPersonRepository.Create(nPerson.NPersonToDal()))
                {
                    await _nPersonHub.RefreshPerson();
                    return Ok(nPerson);
                }
                return BadRequest("Registration Error");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating person: {ex}");
                return StatusCode(500, "Internal server error");
            }


        }
        [HttpDelete("{nPerson_Id}")]
        public IActionResult DeleteNPerson(int nPerson_Id)
        {
            _nPersonRepository.DeleteNPerson(nPerson_Id);
            return Ok();
        }
        [HttpPut("{nPerson_Id}")]
        public IActionResult UpdateNPerson(string lastname, string firstname, string email, string address_Street, string address_Nbr, string postalCode, string address_City, string address_Country, string telephone, string gsm, int nPerson_Id)
        {
            _nPersonRepository.UpdateNPerson(lastname, firstname, email, address_Street, address_Nbr, postalCode, address_City, address_Country, telephone, gsm, nPerson_Id);
            return Ok();
        }
        [HttpPost("update")]
        public IActionResult ReceivePersonUpdate(Dictionary<string, NPersonHub> newUpdate)
        {
            foreach (var item in newUpdate)
            {
                _currentNPerson[item.Key] = item.Value;
            }
            return Ok(_currentNPerson);
        }
    }
}
