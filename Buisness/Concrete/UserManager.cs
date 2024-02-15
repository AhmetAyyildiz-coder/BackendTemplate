using Buisness.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Buisness.Concrete;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
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
}