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
        
        public MapController(IMapRepository mapRepository, MapHub mapHub)
        {
            _mapRepository = mapRepository;
            _mapHub = mapHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMaps()
        {
            try
            {
                var maps = await _mapRepository.GetAllMaps();
                return Ok(maps);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("{map_Id}")]
        public async Task<IActionResult> GetByIdMap(int map_Id)
        {
            try
            {
                var map = await _mapRepository.GetByIdMap(map_Id);
                if (map == null)
                {
                    return NotFound($"Map with ID {map_Id} not found");
                }
                return Ok(map);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving map: {ex.Message}");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create(MapRegisterForm map)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var mapDal = map.MapToDal();
                var mapCrated = _mapRepository.Create(mapDal);

                if (mapCrated)
                {
                    await _mapHub.RefreshMap();

                    return CreatedAtAction(nameof(Create), new { id = mapDal.Map_Id }, mapDal);
                }
                return BadRequest(new { message = "Registration Error. Could not create map" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating Map; {ex}");
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
                
            
        }
        [HttpDelete("{map_Id}")]
        public async Task<IActionResult> DeleteMap(int map_Id)
        {
            try
            {
                var map = await _mapRepository.DeleteMap(map_Id);
                if (map == null)
                {
                    return NotFound($"Map with ID {map_Id} not found");
                }
                return Ok("Map deleted successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error {ex.Message}");
            }

        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateMap(MapUpdate mapUpdate)
        {
            try
            {
                var mapDal = mapUpdate.MapUpdateToDal();
                var updateMap = await _mapRepository.UpdateMap(mapDal);

                if (updateMap == null)
                {
                    return NotFound($"Map with ID {mapDal.Map_Id} not found");
                }

                return Ok(updateMap);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }
}
