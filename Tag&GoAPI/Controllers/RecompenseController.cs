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
        private readonly Dictionary<string, RecompenseHub> _currentRecompense = new Dictionary<string, RecompenseHub>();

        public RecompenseController(IRecompenseRepository recompenseRepository, RecompenseHub recompenseHub)
        {
            _recompenseRepository = recompenseRepository;
            _recompenseHub = recompenseHub;
        }
        [HttpGet]
        public ActionResult GetAllRecompenses()
        {
            return Ok(_recompenseRepository.GetAllRecompenses());
        }
        [HttpGet("{recompense_Id}")]
        public ActionResult GetByIdRecompense(int recompense_Id)
        {
            try
            {
                var recompense = _recompenseRepository.GetByIdRecompense(recompense_Id);
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
                return BadRequest();
            }
            try
            {
                if (_recompenseRepository.Create(recompense.RecompenseToDal()))
                {
                    await _recompenseHub.RefreshRecompense();
                    return Ok(recompense);
                }
                return BadRequest("Registration Error");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating recompense: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpPut("{recompense_Id}")]
        public IActionResult UpdateRecompense(string definition, string point, string implication, string granted, int recompense_Id)
        {
            _recompenseRepository.UpdateRecompense(definition, point, implication, granted, recompense_Id);
            return Ok();
        }
        [HttpPost("update")]
        public IActionResult ReceiveRecompenseUpdate(Dictionary<string, RecompenseHub> newUpdate)
        {
            foreach (var item in newUpdate)
            {
                _currentRecompense[item.Key] = item.Value;
            }
            return Ok(_currentRecompense);
        }
    }
}
