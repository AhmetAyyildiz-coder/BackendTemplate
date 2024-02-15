using Buisness.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto dto)
        {
            var userToLogin = _authService.Login(dto);

            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var res = _authService.CreateToken(userToLogin.Data);

            if (res.Success)
            {
                return Ok(res.Data);
            }

            return BadRequest(res.Message);

        }


        [HttpPost]
        public IActionResult Register(UserForRegisterDto dto)
        {
            var userExist = _authService.UserExist(dto.Email);

            if (userExist.Success)
            {
                return BadRequest("Bu email zaten kayıtlı !");
            }

            var registerResult = _authService.Register(dto);
            var result = _authService.CreateToken(registerResult.Data);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);



        }
    }
}
