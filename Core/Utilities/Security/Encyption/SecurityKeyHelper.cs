using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encyption;


public class SecurityKeyHelper
{
    /// <summary>
    /// JWT (JSON Web Token) oluşturulurken kullanılacak olan güvenlik anahtarını (SecurityKey) oluşturmak için kullanılır.
    /// </summary>
    /// <param name="securityKey"></param>
    /// <returns></returns>
    public static SecurityKey CreateSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }
}