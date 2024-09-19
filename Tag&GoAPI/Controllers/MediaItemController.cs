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
        
        public MediaItemController(IMediaItemRepository mediaItemRepository, MediaItemHub mediaItemHub)
        {
            _mediaItemRepository = mediaItemRepository;
            _mediaItemHub = mediaItemHub;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllMediaItems()
        {
            try
            {
                var mediaitems = await _mediaItemRepository.GetAllMediaItems();
                return Ok(mediaitems);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("{mediaItem_Id}")]
        public async Task<ActionResult> GetById(int mediaItem_Id)
        {
            try
            {
                var mediaItem = await _mediaItemRepository.GetByIdMediaItem(mediaItem_Id);
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
                return BadRequest(ModelState);
            }
            try
            {
                var mediaItemDal = newMediaItem.MediaItemToDal();
                var mediaItemCreated = _mediaItemRepository.Create(mediaItemDal);

                if (mediaItemCreated)
                {
                    await _mediaItemHub.RefreshMediaItem();

                    return CreatedAtAction(nameof(Created), new { id = mediaItemDal.MediaItem_Id }, mediaItemDal);
                }
                return BadRequest(new { message = "Registration Horror. Could not create Media Item" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating media item: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        [HttpDelete("{mediaItem_Id}")]
        public async Task<IActionResult> DeleteMediaItem(int mediaItem_Id)
        {
            try
            {
                var mediaitem = await _mediaItemRepository.DeleteMediaItem(mediaItem_Id);
                if (!ModelState.IsValid)
                {
                    await _mediaItemRepository.DeleteMediaItem(mediaItem_Id);
                }
                return Ok("Deleted");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateMediaItem(MediaItemUpdate mediaItemUpdate)
        {
            var mediaItemDal = mediaItemUpdate.MediaItemUpdateToDal();

            try
            {
                var updateMediaitem = await _mediaItemRepository.UpdateMediaItem(mediaItemDal);

                if (updateMediaitem == null)
                {
                    return NotFound($"Media Item with ID {mediaItemDal.MediaItem_Id} not found.");
                }

                return Ok(updateMediaitem);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
    }
}
