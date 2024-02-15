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

    public IResult ChangePassword(ChangePasswordDto dto)
    {
        var userToCheck = _userService.GetByEmail(dto.Email);

        // Check if the user exists
        if (userToCheck.Data == null)
        {
            return new Result(false, "Kullanıcı bulunamadı");
        }

        // Check if the provided old password is correct
        if (!HashingHelper.VerifyPasswordHash(dto.OldPassword, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
        {
            return new Result(false, "Eski şifre yanlış");
        }

        // Generate new password hash and salt
        byte[] newPasswordHash, newPasswordSalt;
        HashingHelper.CreatePasswordHash(dto.NewPassword, out newPasswordHash, out newPasswordSalt);

        // Update user's password with the new hash and salt
        userToCheck.Data.PasswordHash = newPasswordHash;
        userToCheck.Data.PasswordSalt = newPasswordSalt;

        // Update user entity in the database
        _userService.Update(userToCheck.Data);

        return new Result(true, "Şifre başarıyla değiştirildi");
    }
}