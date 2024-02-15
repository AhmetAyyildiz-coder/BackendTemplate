using AutoMapper;
using Buisness.Abstract;
using Buisness.Constant;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DTOs.Users;
using Entities.Concrete;


namespace Buisness.Concrete;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;
    private readonly IMapper _mapper;

    public UserManager(IUserDal userDal, IMapper mapper)
    {
        _userDal = userDal;
        _mapper = mapper;
    }

    public IDataResult<List<OperationClaim>> GetClaims(User user)
    {
        try
        {
            return new DataResult<List<OperationClaim>>(_userDal.GetClaims(user),true);
        }
        catch (Exception e)
        {
            return new DataResult<List<OperationClaim>>(null, false);
        }
        
    }

    public IResult Add(User user)
    {
        try
        {
            _userDal.Add(user);
            return new Result(true);
        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
    }

    public IDataResult<User?> GetByEmail(string email)
    {
        try
        {
            return new DataResult<User?>(_userDal.Get(u => u.Email == email),true);
        }
        catch (Exception e)
        {
            return new DataResult<User?>(null, false,e.Message);
        }
    }

    public IResult Update(User user)
    {
        try
        {
            _userDal.Update(user);
            return new Result(true);
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
            var user = _userDal.Get(u => u.Email == email);
            _userDal.Delete(user);
            return new Result(true, Messages.UserDeletedSuccess);

        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
    }

    #endregion

}