﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_GoAPI.Hubs;
using Tag_Go.DAL.Interfaces;
using Tag_GoAPI.DTOs.Forms;
using Tag_GoAPI.Tools;
using System.Security.Cryptography;

namespace Tag_GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonusController : ControllerBase
    {
    #nullable disable
        private readonly IBonusRepository _bonusRepository;
        private readonly BonusHub _bonusHub;
        private readonly Dictionary<string, string> _currentBonus = new Dictionary<string, string>();

        public BonusController(IBonusRepository bonusRepository, BonusHub bonusHub)
        {
            _bonusRepository = bonusRepository;
            _bonusHub = bonusHub;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBonuss()
        {
            try
            {
                var bonus = await _bonusRepository.GetAllBonuss();
                return Ok(bonus);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        //[HttpGet("{bonus_Id}")]
        //public async Task<IActionResult> GetById(int bonus_Id)
        //{
        //    try
        //    {
        //        var bonus = await _bonusRepository.GetByIdBonus(bonus_Id);
        //        if (!ModelState.IsValid) 
        //        {
        //            return NotFound();
        //        }
        //        return Ok(_bonusRepository.GetByIdBonus(bonus_Id));
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
        //    }

        //}
        [HttpPost]
        public async Task<IActionResult> Create(BonusRegisterForm bonus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var bonusDal = bonus.BonusToDal();
                var bonusCreated = _bonusRepository.Create(bonusDal);

                if (bonusCreated)
                {
                    await _bonusHub.RefreshBonus();

                    return CreatedAtAction(nameof(Create), new { id = bonusDal.Bonus_Id}, bonusDal);
                }
                return BadRequest(new { message = "Registration Error. Could not create activity" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating bonus: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }

        }
        //[HttpDelete("{bonus_Id}")]
        //public async Task<IActionResult> DeleteBonus(int bonus_Id)
        //{
        //    try
        //    {
        //        var bonus = await _bonusRepository.DeleteBonus(bonus_Id);
        //        if (!ModelState.IsValid)
        //        {
        //            await _bonusRepository.DeleteBonus(bonus_Id);
        //        }

        //        return Ok("Deleted");
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }

        //}
        [HttpPut("update")]
        public async Task<IActionResult> UpdateBonus(BonusUpdate bonusUpdate)
        {
            var bonusDal = bonusUpdate.BonusUpdateToDal();

            try
            {
                var updatedBonus = await _bonusRepository.UpdateBonus(bonusDal);

                if (updatedBonus == null)
                {
                    return NotFound($"Bonus with ID {bonusDal.Bonus_Id} not found.");
                }
                return Ok(updatedBonus);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        //[HttpPost("update")]
        //public async Task <IActionResult> ReceiveBonusUpdate(Dictionary<string, string> newUpdate)
        //{
        //    foreach (var item in newUpdate)
        //    {
        //        try
        //        {
        //            _currentBonus[item.Key] = item.Value;
        //        }
        //        catch (Exception ex)
        //        {

        //            BadRequest(ex.Message);
        //        }

        //    }
        //    return Ok(_currentBonus);
        //}
    }
}
