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
        public async Task<IActionResult> GetAllNPersons()
        {
            try
            {
                var npersons = await _nPersonRepository.GetAllNPersons();
                return Ok(npersons);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        //[HttpGet("{person_Id}")]
        //public async Task<IActionResult> GetByIdNPerson(int nPerson_Id)
        //{
        //    try
        //    {
        //        var nperson = await _nPersonRepository.GetByIdNPerson(nPerson_Id);
        //        if (!ModelState.IsValid) 
        //        {
        //            return NotFound();
        //        }
        //        return Ok(_nPersonRepository.GetByIdNPerson(nPerson_Id));
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
        //    }

        //}
        //[Authorize("AdminPolicy")]
        [HttpPost("create")]
        public async Task<IActionResult> Create(NPersonRegisterForm nPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nPersonDal = nPerson.NPersonToDal();
                var nPersonCreated = _nPersonRepository.Create(nPersonDal);

                if (nPersonCreated)
                {
                    await _nPersonHub.RefreshPerson();

                    return CreatedAtAction(nameof(Create), new { id = nPersonDal.NPerson_Id }, nPersonDal);
                }
                return BadRequest(new { message = "Registration Error. Could not create new person" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating person: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }


        }
        //[HttpDelete("{nPerson_Id}")]
        //public async Task<IActionResult> DeleteNPerson(int nPerson_Id)
        //{
        //    try
        //    {
        //        var nperson = await _nPersonRepository.DeleteNPerson(nPerson_Id);
        //        if (!ModelState.IsValid)
        //        {
        //            await _nPersonRepository.DeleteNPerson(nPerson_Id);
        //        }
        //        return Ok(nperson);
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }
            
        //}
        //[HttpPut("{nPerson_Id}")]
        //public async Task<IActionResult> UpdateNPerson(string lastname, string firstname, string email, string address_Street, string address_Nbr, string postalCode, string address_City, string address_Country, string telephone, string gsm, int nPerson_Id)
        //{
        //    try
        //    {
        //        var nperson = await _nPersonRepository.UpdateNPerson(lastname, firstname, email, address_Street, address_Nbr, postalCode, address_City, address_Country, telephone, gsm, nPerson_Id);
        //        return Ok("Updated");
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }
            
        //}
        //[HttpPost("update")]
        //public async Task<IActionResult> ReceivePersonUpdate(Dictionary<string, NPersonHub> newUpdate)
        //{
        //    foreach (var item in newUpdate)
        //    {
        //        try
        //        {
        //            _currentNPerson[item.Key] = item.Value;
        //        }
        //        catch (Exception ex)
        //        {

        //            BadRequest(ex.Message);
        //        }
                
        //    }
        //    return Ok(_currentNPerson);
        //}
    }
}
