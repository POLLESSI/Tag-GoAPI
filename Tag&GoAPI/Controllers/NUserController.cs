using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_GoAPI.Tools;
using Tag_GoAPI.DTOs.Forms;
using Tag_Go.DAL.Interfaces;
using Tag_Go.DAL.Entities;
//using Tag_Go.BLL.Models;
//using Microsoft.AspNetCore.Authorization;
using Tag_GoAPI.Hubs;
using System.Security.Cryptography;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using System.Text.RegularExpressions;

namespace Tag_GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NUserController : ControllerBase
    {
    #nullable disable
        private readonly INUserRepository _userRepository;
        private readonly TokenGenerator _tokenGenerator;
        private readonly NUserHub _nUserHub;
        private readonly Dictionary<string, string> _currentNUser = new Dictionary<string, string>();

        public NUserController(INUserRepository userRepository, TokenGenerator tokenGenerator, NUserHub nUserHub)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _nUserHub = nUserHub;
        }
        //[HttpGet]
        //public async Task<ActionResult> GetAllNUsers()
        //{
        //    try
        //    {
        //        var nusers = await _userRepository.GetAllNUsers();
        //        return Ok(nusers);
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }
            
        //}
        //[HttpGet("{nuser_Id}")]
        //public async Task<ActionResult> GetById(int nUser_Id)
        //{
        //    try
        //    {
        //        var nuser = await _userRepository.GetByIdNUser(nUser_Id);
        //        if (!ModelState.IsValid)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(_userRepository.GetByIdNUser(nUser_Id));
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        //    }

        //}
        //[HttpPost("login")]
        //public IActionResult LoginNUser(NUserRegisterForm nUser)
        //{
        //    try
        //    {
        //        NUser connectedNUser = _userRepository.LoginNUser(nUser.Email, nUser.Pwd);
        //        string MdpNUser = nUser.Pwd;
        //        string hashpwd = connectedNUser.Pwd;
        //        bool motDePassValide = BCrypt.Net.BCrypt.Verify(MdpNUser, hashpwd);
        //        if (motDePassValide)
        //        {
        //            return Ok(_tokenGenerator.GenerateToken(connectedNUser));
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpPost("register")]
        //public IActionResult RegisterNUser(NewNUser nUser)
        //{
        //    _userRepository.RegisterNUser(nUser.Email, nUser.Pwd, nUser.NPerson_Id, nUser.Role_Id, nUser.Avatar_Id, nUser.Point);
        //    return Ok();
        //}
        [HttpPost]
        public async Task<IActionResult> Create(NUserRegisterForm nUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                if (_userRepository.Create(nUser.NUserToDal()))
                {
                    await _nUserHub.RefreshNUser();
                    return Ok();
                }
                return BadRequest("Registration Error");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating user: {ex}");
                return StatusCode(500, "Internal server error");
            }

        }
        //[HttpDelete("{nuser_Id}")]
        //[Route("{nuser_Id: int}")]
        //public async Task<IActionResult> DeleteNUser(int nUser_Id)
        //{
        //    try
        //    {
        //        var nuser = await _userRepository.DeleteNUser(nUser_Id);
        //        if (ModelState.IsValid)
        //        {
        //            await _userRepository.DeleteNUser(nUser_Id);
        //        }
        //        return Ok("Deleted");
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }
            
        //}
        //[HttpPut("nuser_Id")]
        //public async Task<IActionResult> UpdateNUser(int nUser_Id, string email, string pwd, int nPerson_Id, string role_Id, int avatar_Id, string point)
        //{
        //    try
        //    {
        //        var nuser = await _userRepository.UpdateNUser(nUser_Id, email, pwd, nPerson_Id, role_Id, avatar_Id, point);
        //        return Ok("Updated");
        //    }
        //    catch (Exception ex)
        //    {

        //        return StatusCode(500, ex.Message);
        //    }
            
        //}
        //[HttpPost("update")]
        //public async Task<IActionResult> ReceiveNUserUpdate(Dictionary<string, string> newUpdate)
        //{
        //    foreach (var item in newUpdate)
        //    {
        //        try
        //        {
        //            _currentNUser[item.Key] = item.Value;
        //        }
        //        catch (Exception ex)
        //        {

        //            BadRequest(ex.Message);
        //        }
                
        //    }
        //    return Ok(_currentNUser);
        //}
        //[HttpPatch("setRole")]
        //public async Task<IActionResult> ChangeRole(ChangeRole role)
        //{
        //    _userRepository.SetRole(role.NUser_Id, role.Role_Id);
        //    return Ok("Rôle Changed");
        //}
    }
}
