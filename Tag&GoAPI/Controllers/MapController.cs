using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_GoAPI.DTOs.Forms;
using Tag_GoAPI.Hubs;
using Tag_GoAPI.Tools;
using Tag_Go.DAL.Interfaces;
//using System.Runtime.CompilerServices;
using System.Security.Cryptography;
//using System.Reflection.Metadata.Ecma335;

namespace Tag_GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
    #nullable disable
        private readonly IMapRepository _mapRepository;
        private readonly MapHub _mapHub;
        private readonly Dictionary<string, string> _currentMap = new Dictionary<string, string>();

        public MapController(IMapRepository mapRepository, MapHub mapHub)
        {
            _mapRepository = mapRepository;
            _mapHub = mapHub;
        }
        [HttpGet]
        public IActionResult GetAllMaps()
        {
            return Ok(_mapRepository.GetAllMaps());
        }
        [HttpGet("{map_Id}")]
        public IActionResult GetByIdMap(int map_Id)
        {
            try
            {
                var map = _mapRepository.GetByIdMap(map_Id);
                if (!ModelState.IsValid) 
                {
                    return NotFound();
                }
                return Ok(_mapRepository.GetByIdMap(map_Id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Create(MapRegisterForm map)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (_mapRepository.Create(map.MapToDal()))
            {
                await _mapHub.RefreshMap();
                return Ok();
            }
            return BadRequest("Registration Error");
        }
        [HttpDelete("{map_Id}")]
        public IActionResult DeleteMap(int map_Id)
        {
            _mapRepository.DeleteMap(map_Id);
            return Ok();
        }
        [HttpPut("update")]
        public IActionResult UpdateMap(int map_Id, DateTime dateCreation, string mapUrl, string description)
        {
            _mapRepository.UpdateMap(map_Id, dateCreation, mapUrl, description);
            return Ok();
        }
        [HttpPost("update")]
        public IActionResult ReceiveMapUpdate(Dictionary<string, string> newUpdate)
        {
            foreach (var item in newUpdate)
            {
                _currentMap[item.Key] = item.Value;
            }
            return Ok(_currentMap);
        }
    }
}
