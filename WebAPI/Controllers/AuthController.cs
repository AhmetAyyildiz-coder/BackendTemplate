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

        [HttpPost]
        public IActionResult Login(UserForLoginDto dto)
        {
            var userToLogin = _authService.Login(dto);

            // buraya girerse sistemsel hata vardır. 
            if (!userToLogin.Success)
                return BadRequest(userToLogin.Message);

            var res = _authService.CreateToken(userToLogin.Data);

            if (!res.Success)
                return BadRequest(res.Message);
            
            return Ok(res.Data);
        }


        [HttpPost]
        public IActionResult Register(UserForRegisterDto dto)
        {
            var userExist = _authService.UserExist(dto.Email);

            if (userExist.Success)
                return BadRequest("Bu email zaten kayıtlı !");

            var registerResult = _authService.Register(dto);
            var result = _authService.CreateToken(registerResult.Data);

            if (result.Success)
                return Ok(result.Data);

            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordDto dto)
        {
            // Validate incoming DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Attempt to change the user's password
            var changePasswordResult = _authService.ChangePassword(dto);

            if (!changePasswordResult.Success)
            {
                return BadRequest(changePasswordResult.Message);
            }

            return Ok(changePasswordResult); // You may choose to return a success message or any relevant data
        }
    }
}
