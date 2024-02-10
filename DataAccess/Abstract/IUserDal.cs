using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IUserDal : IEntityRepository<User>
{
    /// <summary>
    /// Gelen kullanıcının sahip olduğu rolleri getirir.
    /// </summary>
    /// <param name="User"></param>
    /// <returns></returns>
    List<OperationClaim> GetClaims(User User);
}