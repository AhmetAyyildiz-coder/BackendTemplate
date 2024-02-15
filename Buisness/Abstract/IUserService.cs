using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Buisness.Abstract;

public interface IUserService
{
    IDataResult<List<OperationClaim>> GetClaims(User user);
    IResult Add(User user);
    IDataResult<User?> GetByEmail(string email);
}