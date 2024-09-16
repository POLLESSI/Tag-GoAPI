using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_GoAPI.Hubs;
using Tag_Go.DAL.Interfaces;
using Tag_GoAPI.DTOs.Forms;
using Tag_GoAPI.Tools;
using System.Security.Cryptography;
using Tag_Go.DAL.Entities;
using Tag_GoAPI.DTOs;
//using System.Reflection.Metadata.Ecma335;

namespace Tag_GoAPI.Controllers
{
#nullable disable
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
    #nullable disable
        private readonly IActivityRepository _activityRepository;
        private readonly ActivityHub _activityHub;
        private readonly Dictionary<string, string> _currentActivity = new Dictionary<string, string>();

        public ActivityController(IActivityRepository activityRepository, ActivityHub activityHub)
        {
            _activityRepository = activityRepository;
            _activityHub = activityHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActivities()
        {
            try
            {
                var activities = await _activityRepository.GetAllActivities();
                return Ok(activities);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        //[HttpGet("{activity_Id}")]
        //public async Task<IActionResult> GetByIdActivity(int activity_Id)
        //{
        //    try
        //    {
        //        var activity = await _activityRepository.GetByIdActivity(activity_Id);
        //        if (!ModelState.IsValid)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(_activityRepository.GetByIdActivity(activity_Id));
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
        //    }
        //}
        [HttpPost("create")]
        public async Task<IActionResult> Create(ActivityRegisterForm activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Renvoie les erreurs de validation du modèle
            }

            try
            {
                var activityDal = activity.ActivityToDal();
                var activityCreated = _activityRepository.Create(activityDal);

                if (activityCreated)
                {
                    await _activityHub.RefreshActivity();

                    // Retourne l'objet créé avec un statut 201 (Created)
                    return CreatedAtAction(nameof(Create), new { id = activityDal.Activity_Id }, activityDal);
                }

                return BadRequest(new { message = "Registration Error. Could not create activity" });  // Renvoie une erreur spécifique
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Activity; {ex}");
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });  // Renvoie un message d'erreur avec des détails
            }
        }

        //[HttpDelete("{activity_Id}")]
        //public async Task<IActionResult> DeleteActivity(int activity_Id)
        //{
        //    try
        //    {
        //        var activity = await _activityRepository.DeleteActivity(activity_Id);
        //        if (ModelState.IsValid)
        //        {
        //            await _activityRepository.DeleteActivity(activity_Id);
        //        }
        //        return Ok("Deleted");
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }

        //}
        //[HttpPut("{activity_Id}")]
        //public async Task<IActionResult> UpdateActivity(int activity_Id, string activityName, string activityAddress, string activityDescription, string ComplementareInformation, string posLat, string posLong, int organisateur_Id)
        //{
        //    try
        //    {
        //        var activity = await _activityRepository.UpdateActivity(activity_Id, activityName, activityAddress, activityDescription, ComplementareInformation, posLat, posLong, organisateur_Id);

        //        return Ok("Updated");
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }

        //}
        //[HttpPost("update")]
        //public async Task<IActionResult> ReceiveActivityUpdate(Dictionary<string, string> newUpdate)
        //{
        //    foreach (var item in newUpdate)
        //    {
        //        try
        //        {
        //            _currentActivity[item.Key] = item.Value;
        //        }
        //        catch (Exception ex)
        //        {

        //            BadRequest(ex.Message);
        //        }

        //    }
        //    return Ok(_currentActivity);
        //}
    }
}
