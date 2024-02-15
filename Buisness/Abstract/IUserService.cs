using Core.Entities;
using Core.Utilities.Results;


namespace Buisness.Abstract;

public interface IUserService
{
    IDataResult<List<OperationClaim>> GetClaims(User user);
    IResult Add(User user);
    IDataResult<User?> GetByEmail(string email);
    IResult Update(User user);
}