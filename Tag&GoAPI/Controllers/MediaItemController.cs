using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_GoAPI.Hubs;
using Tag_Go.DAL.Interfaces;
using Tag_GoAPI.DTOs.Forms;
using Tag_GoAPI.Tools;
//using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace Tag_GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaItemController : ControllerBase
    {
    #nullable disable
        private readonly IMediaItemRepository _mediaItemRepository;
        private readonly MediaItemHub _mediaItemHub;
        private readonly Dictionary<string, MediaItemHub> _currentMediaItem = new Dictionary<string, MediaItemHub>();

        public MediaItemController(IMediaItemRepository mediaItemRepository, MediaItemHub mediaItemHub)
        {
            _mediaItemRepository = mediaItemRepository;
            _mediaItemHub = mediaItemHub;
        }
        [HttpGet]
        public ActionResult GetAllMediaItems()
        {
            return Ok(_mediaItemRepository.GetAllMediaItems());
        }
        [HttpGet("{mediaItem_Id}")]
        public ActionResult GetById(int mediaItem_Id)
        {
            try
            {
                var mediaItem = _mediaItemRepository.GetByIdMediaItem(mediaItem_Id);
                if (!ModelState.IsValid) 
                { 
                    return NotFound();
                }
                return Ok(_mediaItemRepository.GetByIdMediaItem(mediaItem_Id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(MediaItemRegisterForm newMediaItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                if (_mediaItemRepository.Create(newMediaItem.MediaItemToDal()))
                {
                    await _mediaItemHub.RefreshMediaItem();
                    return Ok(newMediaItem);
                }
                return BadRequest("Registration Horror");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating media item: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpDelete("{mediaItem_Id}")]
        public IActionResult DeleteMediaItem(int mediaItem_Id)
        {
            _mediaItemRepository.DeleteMediaItem(mediaItem_Id);
            return Ok();
        }
        [HttpPut("{mediaItem_Id}")]
        public IActionResult UpdateMediaItem(int mediaItem_Id, string mediaType, string urlItem, string description)
        {
            _mediaItemRepository.UpdateMediaItem(mediaItem_Id, mediaType, urlItem, description);
            return Ok();
        }
        [HttpPost("update")]
        public IActionResult ReceiveMediaItemUpdate(Dictionary<string, MediaItemHub> newUpdate)
        {
            foreach (var item in newUpdate)
            {
                _currentMediaItem[item.Key] = item.Value;
            }
            return Ok(_currentMediaItem);
        }
    }
}
