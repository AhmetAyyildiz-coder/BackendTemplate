using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encyption;

/// <summary>
///
/// </summary>
public class SigningCredentialsHelper
{
    /// <summary>
    ///  WT (JSON Web Token) oluşturulurken kullanılacak olan imza anahtarlarını oluşturmak için kullanılır.
    /// </summary>
    /// <param name="key">Security Key</param>
    /// <returns></returns>
    public static SigningCredentials CreateSigningCredentials(SecurityKey key)
    {
        return new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
    }
}