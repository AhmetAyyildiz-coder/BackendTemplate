using Buisness.Abstract;
using Buisness.Constant;
using DTOs.OperationClaims;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Roles.Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AdminController(IUserService service, IAuthService authService)
        {
            _userService = service;
            _authService = authService;
        }



        [HttpGet]
        /// <summary>
        /// Sistemde kayıtlı olan bütün kullanıcıları döndürür.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllSystemUser()
        {
            var users = _authService.GetAllSystemUser();
            if (!users.Success)
            {
                return BadRequest();
            }

            return Ok(users);
        }


        [HttpGet]
        public IActionResult RemoveUser(string UserEmail)
        {
            var userExist = _authService.UserExist(UserEmail);

            if (!userExist.Success)
            {
                return BadRequest(userExist);
            }

             var result = _authService.RemoveUser(UserEmail);

             if (!result.Success)
             {
                 return BadRequest(result);
             }

             return Ok(result);

        }


        [HttpPost]
        public IActionResult AddOperationClaim([FromBody] OperationClaimDto dto)
        {
           var result = _authService.AddOperationClaim(dto);

           if (!result.Success)
           {
               return BadRequest(result);
           }

           return Ok(result);
        }
    }
}
