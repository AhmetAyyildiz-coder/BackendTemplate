using AutoMapper;
using Buisness.Abstract;
using Buisness.Constant;
using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DTOs.OperationClaims;
using DTOs.Users;



namespace Buisness.Concrete;

public class AuthManager : IAuthService
{

    private readonly IUserDal _userDal;
    private readonly ITokenHelper _tokenHelper;
    private readonly IMapper _mapper;
    private readonly IOperationClaimDal _operationClaimDal;
    private readonly IUserOperationClaimDal _userOperationClaimDal;

    public AuthManager(ITokenHelper tokenHelper, IUserDal userDal, IMapper mapper, IOperationClaimDal operationClaimDal, IUserOperationClaimDal userOperationClaimDal)
    {
        
        _tokenHelper = tokenHelper;
        _userDal = userDal;
        _mapper = mapper;
        _operationClaimDal = operationClaimDal;
        _userOperationClaimDal = userOperationClaimDal;
    }

    public IDataResult<UserForRegisterDto> Register(UserForRegisterDto dto)
    {
        try
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

            _userDal.Add(user);
            return new DataResult<UserForRegisterDto>(_mapper.Map<UserForRegisterDto>(user), true);

        }
        catch (Exception e)
        {
            return new DataResult<UserForRegisterDto>(null, false, Messages.RegisterFailed);
        }

    }

    public IDataResult<UserForLoginDto> Login(UserForLoginDto dto)
    {
        var userToCheck = _userDal.GetByEmail(dto.Email);
        if (userToCheck is null)
            return new DataResult<UserForLoginDto>(null, false, Messages.UserNotFound);


        if (!HashingHelper.VerifyPasswordHash(dto.Password, userToCheck.PasswordHash,
                userToCheck.PasswordSalt))
        {
            return new DataResult<UserForLoginDto>(null, false, Messages.WrongPassword);
        }


        return new DataResult<UserForLoginDto>(_mapper.Map<UserForLoginDto>(userToCheck), true);
    }

    public IResult UserExist(string email)
    {
        if (_userDal.GetByEmail(email) is null)
        {
            return new Result(false, message:Messages.UserNotFound);
        }

        return new Result(true);

    }

    public IDataResult<AccessToken> CreateToken(string email)
    {
        var user = _userDal.GetByEmail(email);
        if (user is null)
            return new DataResult<AccessToken>(null, false, Messages.UserNotFound);


        var operationClaims = _userDal.GetClaims(user);
        var token =  _tokenHelper.CreateToken(user, operationClaims);
        return new DataResult<AccessToken>(token, true);
    }

    public IResult ChangePassword(ChangePasswordDto dto)
    {
        try
        {
            var userToCheck = _userDal.GetByEmail(dto.Email);

            // Check if the user exists
            if (userToCheck is null)
                return new Result(false, Messages.UserNotFound);


            // Check if the provided old password is correct
            if (!HashingHelper.VerifyPasswordHash(dto.OldPassword
                    , userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new Result(false, Messages.WrongPassword);
            }

            // Generate new password hash and salt
            byte[] newPasswordHash, newPasswordSalt;
            HashingHelper.CreatePasswordHash(dto.NewPassword, out newPasswordHash, out newPasswordSalt);

            // Update user's password with the new hash and salt
            userToCheck.PasswordHash = newPasswordHash;
            userToCheck.PasswordSalt = newPasswordSalt;

            // Update user entity in the database
            _userDal.Update(userToCheck);

            return new Result(true, Messages.ChangePasswordSuccess);
        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
    }


    #region Admin Methods 

    public IDataResult<List<UserListDto>> GetAllSystemUser()
    {
        try
        {
            var users = _userDal.GetList();
            var userDtos = _mapper.Map<List<UserListDto>>(users);
            return new DataResult<List<UserListDto>>(userDtos, true);
        }
        catch (Exception e)
        {

            return new DataResult<List<UserListDto>>(null, false, e.Message);
        }

    }


    public IResult RemoveUser(string email)
    {
        try
        {
            var user = _userDal.GetByEmail(email);
            _userDal.Delete(user);
            return new Result(true, Messages.UserDeletedSuccess);

        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
    }

    public IResult AddOperationClaim(OperationClaimDto dto)
    {
        try
        {
            var operationClaim = _mapper.Map<OperationClaim>(dto);
            _operationClaimDal.Add(operationClaim);
            return new Result(true, Messages.AddedOperationClaim);
        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
        
    }

    /// <summary>
    /// Check userId - before method invoke
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="operationClaimId"></param>
    /// <returns></returns>
    public IResult AddOperationClaimOnUser(int userId, int operationClaimId)
    {
        try
        {
            var operationClaim = _operationClaimDal.Get(o => o.Id == operationClaimId);
            _userOperationClaimDal.Add(new UserOperationClaim()
            {
                OperationClaimId = operationClaimId,
                UserId = userId
            });

            return new Result(true, Messages.AddedOperationClaimForUser);
        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }

    }

    #endregion
}