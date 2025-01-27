﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_GoAPI.Hubs;
using Tag_Go.DAL.Interfaces;
using Tag_GoAPI.DTOs.Forms;
using Tag_GoAPI.Tools;
using System.Security.Cryptography;
using Tag_Go.DAL.Entities;
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
        public IActionResult GetAllActivities()
        {
            return Ok(_activityRepository.GetAllActivities());
        }
        [HttpGet("{activity_Id}")]
        public IActionResult GetByIdActivity(int activity_Id)
        {
            try
            {
                var activity = _activityRepository.GetByIdActivity(activity_Id);
                if (!ModelState.IsValid)
                {
                    return NotFound();
                }
                return Ok(_activityRepository.GetByIdActivity(activity_Id));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(ActivityRegisterForm activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                if (_activityRepository.Create(activity.ActivityToDal()))
                {
                    await _activityHub.RefreshActivity();
                    return Ok();
                }
                return BadRequest("Registration Error");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating Activity; {ex}");
                return StatusCode(500, "Internal Server error");
            }

        }
        [HttpDelete("{activity_Id}")]
        public IActionResult DeleteActivity(int activity_Id)
        {
            _activityRepository.DeleteActivity(activity_Id);
            return Ok();
        }
        [HttpPut("{activity_Id}")]
        public IActionResult UpdateActivity(int activity_Id, string activityName, string activityAddress, string activityDescription, string ComplementareInformation, string posLat, string posLong, int organisateur_Id)
        {
            _activityRepository.UpdateActivity(activity_Id, activityName, activityAddress, activityDescription, ComplementareInformation, posLat, posLong, organisateur_Id);
            return Ok();
        }
        [HttpPost("update")]
        public IActionResult ReceiveActivityUpdate(Dictionary<string, string> newUpdate)
        {
            foreach (var item in newUpdate)
            {
                _currentActivity[item.Key] = item.Value;
            }
            return Ok(_currentActivity);
        }
    }
}
