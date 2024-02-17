using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DTOs.OperationClaims;
using DTOs.Users;

namespace Buisness.Abstract;

public interface IAuthService
{
    IDataResult<UserForRegisterDto> Register(UserForRegisterDto dto);
    IDataResult<UserForLoginDto> Login(UserForLoginDto dto);
    /// <summary>
    /// Eğer sistemde gelen email ile ilgili kullanıcı varsa true döner. Aksi halde false döner.
    /// </summary>
    /// <param name="email">Kullanıcının email adresi</param>
    /// <returns></returns>
    IResult UserExist(string email);
    IDataResult<AccessToken> CreateToken(string email);
    IResult ChangePassword(ChangePasswordDto dto);

    IDataResult<List<UserListDto>> GetAllSystemUser();

    IResult RemoveUser(string email);

    IResult AddOperationClaim(OperationClaimDto dto);
}