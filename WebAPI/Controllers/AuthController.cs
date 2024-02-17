using Buisness.Abstract;
using DTOs.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserForLoginDto dto)
        {
            var userToLogin = _authService.Login(dto);

            // buraya girerse sistemsel hata vardır. 
            if (!userToLogin.Success)
                return BadRequest(userToLogin.Message);

            var res = _authService.CreateToken(userToLogin.Data.Email);

            if (!res.Success)
                return BadRequest(res);

            return Ok(res);
        }


        [HttpPost]
        public IActionResult Register(UserForRegisterDto dto)
        {
            var userExist = _authService.UserExist(dto.Email);

            if (userExist.Success)
                return BadRequest("Bu email zaten kayıtlı !");

            var registerResult = _authService.Register(dto);
            var result = _authService.CreateToken(registerResult.Data.Email);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
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
                return BadRequest(changePasswordResult);
            }

            return Ok(changePasswordResult); // You may choose to return a success message or any relevant data
        }

        
    }
}
