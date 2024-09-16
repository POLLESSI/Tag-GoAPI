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
        //[HttpGet("{map_Id}")]
        //public async Task<IActionResult> GetByIdMap(int map_Id)
        //{
        //    try
        //    {
        //        var map = await _mapRepository.GetByIdMap(map_Id);
        //        if (!ModelState.IsValid) 
        //        {
        //            return NotFound();
        //        }
        //        return Ok(_mapRepository.GetByIdMap(map_Id));
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        //    }

        //}
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
        //[HttpDelete("{map_Id}")]
        //public async Task<IActionResult> DeleteMap(int map_Id)
        //{
        //    try
        //    {
        //        var map = await _mapRepository.DeleteMap(map_Id);
        //        if (!ModelState.IsValid)
        //        {
        //            await _mapRepository.DeleteMap(map_Id);
        //        }
        //        return Ok("Deleted");
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
            
        //}
        //[HttpPut("map_Id")]
        //public async Task<IActionResult> UpdateMap(int map_Id, DateTime dateCreation, string mapUrl, string description)
        //{
        //    try
        //    {
        //        var map = await _mapRepository.UpdateMap(map_Id, dateCreation, mapUrl, description);
        //        return Ok("Updated");
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }
            
        //}
        //[HttpPost("update")]
        //public async Task<IActionResult> ReceiveMapUpdate(Dictionary<string, string> newUpdate)
        //{
        //    foreach (var item in newUpdate)
        //    {
        //        try
        //        {
        //            _currentMap[item.Key] = item.Value;
        //        }
        //        catch (Exception ex)
        //        {

        //            BadRequest(ex.Message);
        //        }
                
        //    }
        //    return Ok(_currentMap);
        //}
    }
}
