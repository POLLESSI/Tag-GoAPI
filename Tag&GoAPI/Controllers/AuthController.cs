using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tag_Go.BLL.Interfaces;

namespace Tag_GoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
    #nullable disable
        private readonly INUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = (INUserService?)userService;
        }

        //[HttpPost("register")]
        //public async Task<ActionResult> Register(UserRegisterDto dto)
        //{
        //    await _userService.RegisterAsync(dto);
        //    return Ok();
        //}

        //[HttpPost("login")]
        //public async Task<ActionResult<string>> Login(UserLoginDto dto)
        //{
        //    var token = await _userService.LoginAsync(dto);
        //    return Ok(token);
        //}
    }
}
