using Core.Entities;

namespace Core.Utilities.Security.JWT;


public interface ITokenHelper
{
    /// <summary>
    /// Sadece kullanıcı için token oluşturma
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    AccessToken CreateToken(User user,List<OperationClaim> operationClaims);
    
    /// <summary>
    /// Kullanıcıya ek olarak role bazlı token oluşturma
    /// </summary>
    /// <param name="user"></param>
    /// <param name="claims"></param>
    /// <returns></returns>
    AccessToken CreateTokenWithRoles(User user, List<OperationClaim> claims);
}