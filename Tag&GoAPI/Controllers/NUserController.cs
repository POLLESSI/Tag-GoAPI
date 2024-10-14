using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_GoAPI.Tools;
using Tag_GoAPI.DTOs.Forms;
using Tag_Go.DAL.Interfaces;
using Tag_Go.DAL.Entities;
using Tag_GoAPI.Hubs;
using System.Security.Cryptography;

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
        
        public NUserController(INUserRepository userRepository, TokenGenerator tokenGenerator, NUserHub nUserHub)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _nUserHub = nUserHub;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllNUsers()
        {
            try
            {
                bool isAdmin = User.IsInRole("Admin");
                var nusers = await _userRepository.GetAllNUsers(isAdmin);
                if (!nusers.Any())
                {
                    return NotFound("No activive users found.");
                }
                return Ok(nusers);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpGet("{nuser_Id}")]
        public async Task<ActionResult> GetById(int nUser_Id)
        {
            try
            {
                var nuser = await _userRepository.GetByIdNUser(nUser_Id);
                if (nuser == null)
                {
                    return NotFound($"User with ID {nUser_Id} not found");
                }
                return Ok(nuser);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving User: {ex.Message}");
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginNUser(NUserLoginForm nUser)
        {
            try
            {
                NUser connectedNUser = await _userRepository.LoginNUser(nUser.Email, nUser.Pwd);
                string MdpNUser = nUser.Pwd;
                string hashpwd = connectedNUser.Pwd;
                bool motDePassValide = BCrypt.Net.BCrypt.Verify(MdpNUser, hashpwd);
                if (motDePassValide)
                {
                    return Ok(_tokenGenerator.GenerateToken(connectedNUser));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNUser(NUserRegisterForm nUser)
        {
            //NUser registredNUser = await _userRepository.RegisterNUser(nUser.Email, nUser.Pwd, nUser.NPerson_Id, nUser.Role_Id, nUser.Avatar_Id, nUser.Point);
            return Ok();
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(NUserRegisterForm nUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var nUserDal = nUser.NUserToDal();
                Tag_Go.DAL.Entities.NUser nUserCreated = await _userRepository.Create(nUserDal);

                if (nUserCreated != null)
                {
                    await _nUserHub.RefreshNUser();

                    return CreatedAtAction(nameof(Create), new { id = nUserCreated.NUser_Id }, nUserCreated);
                }
                return BadRequest(new { message = "Registration Error. Could not create new User" });
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating user: {ex}");
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
        [HttpDelete("{nuser_Id}")]
        public async Task<IActionResult> DeleteNUser(int nUser_Id)
        {
            try
            {
                var nuser = await _userRepository.DeleteNUser(nUser_Id);
                if (nuser == null)
                {
                    return NotFound($"User with ID {nUser_Id} not found");
                }
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error {ex.Message}");
            }

        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateNUser(NUserUpdate nUserUpdate)
        {
            try
            {
                var nUserDal = nUserUpdate.NUserUpdateToDal();
                var updateNUser = await _userRepository.UpdateNUser(nUserDal);

                if (updateNUser == null)
                {
                    return NotFound($"User with ID {nUserDal.NUser_Id} not found.");
                }

                return Ok(updateNUser);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }
        [HttpPatch("setRole")]
        public async Task<IActionResult> ChangeRole(NUserChangeRole role)
        {
            try
            {
                _userRepository.SetRole(role.NUser_Id, role.Role_Id);
                return Ok("Rôle Changed");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error {ex.Message}");
            }
            
        }
    }
}
