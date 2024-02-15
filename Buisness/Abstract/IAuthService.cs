using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Buisness.Abstract;

public interface IAuthService
{
    IDataResult<User> Register(UserForRegisterDto dto);
    IDataResult<User> Login(UserForLoginDto dto);
    /// <summary>
    /// Eğer sistemde gelen email ile ilgili kullanıcı varsa true döner. Aksi halde false döner.
    /// </summary>
    /// <param name="email">Kullanıcının email adresi</param>
    /// <returns></returns>
    IResult UserExist(string email);
    IDataResult<AccessToken> CreateToken(User user);
}