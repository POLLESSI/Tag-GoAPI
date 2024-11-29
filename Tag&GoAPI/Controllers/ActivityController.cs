using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_GoAPI.Hubs;
using Tag_Go.DAL.Interfaces;
using Tag_GoAPI.DTOs.Forms;
using Tag_GoAPI.Tools;
using System.Security.Cryptography;
using Tag_Go.DAL.Entities;
using Tag_GoAPI.DTOs;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;

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
                bool isAdmin = User.IsInRole("Admin");
                var activities = await _activityRepository.GetAllActivities();

                if (!activities.Any()) 
                {
                    return NotFound("No active activies found.");
                }

                return Ok(activities);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpGet("{activity_Id}")]
        public async Task<IActionResult> GetByIdActivity(int activity_Id)
        {
            try
            {
                var activity = await _activityRepository.GetByIdActivity(activity_Id);
                if (activity == null)
                {
                    return NotFound($"Activity with ID {activity_Id} not found");
                }
                return Ok(activity);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving activity: {ex.Message}");
            }
        }
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
                Tag_Go.DAL.Entities.Activity activityCreated = await _activityRepository.Create(activityDal);

                if (activityCreated != null)
                {
                    await _activityHub.RefreshActivity();

                    // Retourne l'objet créé avec un statut 201 (Created)
                    return CreatedAtAction(nameof(Create), new { activity_id = activityCreated.Activity_Id }, activityCreated);
                }

                return BadRequest(new { message = "Registration Error. Could not create activity" });  // Renvoie une erreur spécifique
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Activity; {ex}");
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });  // Renvoie un message d'erreur avec des détails
            }
        }

        [HttpDelete("{activity_Id}")]
        public async Task<IActionResult> DeleteActivity(int activity_Id)
        {
            try
            {
                var activity = await _activityRepository.DeleteActivity(activity_Id);
                if (activity == null)
                {
                    return NotFound($"Activity with ID {activity_Id} not found");
                }
                return Ok("Activity deleted successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error {ex.Message}");
            }

        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateActivity(ActivityUpdate activityUpdate)
        {
            
            
            try
            {
                var activityDal = activityUpdate.ActivityUpdateToDal();
                // Mise à jour de l'activité dans le dépôt
                var updatedActivity = await _activityRepository.UpdateActivity(activityDal);

                // Vérifier si l'activité existe et a été mise à jour
                if (updatedActivity == null)
                {
                    return NotFound($"Activity with ID {activityDal.Activity_Id} not found.");
                }
                
                // Retourner l'activité mise à jour
                return Ok(updatedActivity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
