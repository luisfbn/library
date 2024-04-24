using Library.Entities;
using Library.Services.DTOs;
using Library.Services.Interfaces;
using Library.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;


        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateRequestDto request)
        {
            var userDto = _userService.Authenticate(request);

            if (userDto == null)
                return BadRequest(new { message = "User o contraseña incorrectos" });

            return Ok(userDto);
        }


        //[HttpPost("register")]
        //public async Task<ActionResult<User>> Register(UserDto request)
        //{
        //    var result = _userService.CreatePasswordHash
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result.ErrorMessage);
        //    return Ok(user);
        //}

        //[HttpPost("login")]
        //public async Task<ActionResult<string>> Login(UserDto request)
        //{
        //    if (user.Username != request.username)
        //    {
        //        return BadRequest("User not found.");
        //    }

        //    if (!VerifyPasswordHash(request.password, user.PasswordHash, user.PasswordSalt))
        //    {
        //        return BadRequest("Password is wrong!");
        //    }

        //    string token = CreateToken(user);

        //    return Ok(token);
        //}

    }
}
