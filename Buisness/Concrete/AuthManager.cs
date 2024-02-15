using Buisness.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Buisness.Concrete;

public class AuthManager : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
    }

    public IDataResult<User> Register(UserForRegisterDto dto)
    {
        byte[] passwordHash, paswordSalt;
        HashingHelper.CreatePasswordHash(dto.Password, out passwordHash, out paswordSalt);

        var user = new User
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = paswordSalt,
            Status = true
        };

        _userService.Add(user);

        return new DataResult<User>(user, true);

    }

    public IDataResult<User> Login(UserForLoginDto dto)
    {
        var userToCheck = _userService.GetByEmail(dto.Email);

        if (!HashingHelper.VerifyPasswordHash(dto.Password, userToCheck.Data.PasswordHash,
                userToCheck.Data.PasswordSalt))
        {
            return new DataResult<User>(null, false, "Password Yanlış ");
        }

        return new DataResult<User>(userToCheck.Data, true);
    }

    public IResult UserExist(string email)
    {
        if (_userService.GetByEmail(email).Data == null)
        {
            return new Result(false, "Bu kullanıcı bulunamadı");
        }

        return new Result(true);

    }

    public IDataResult<AccessToken> CreateToken(User user)
    {
        var operationClaims = _userService.GetClaims(user).Data;
        var token =  _tokenHelper.CreateToken(user, operationClaims);
        return new DataResult<AccessToken>(token, true);
    }
}