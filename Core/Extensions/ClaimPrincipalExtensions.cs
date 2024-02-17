using System.Security.Claims;

namespace Core.Extensions;

public static class ClaimPrincipalExtensions
{
    /// <summary>
    /// Kullanıcı rollerini dönebilmemizi sağlayan - filtreleme sağlayan yapıdır.
    /// </summary>
    /// <param name="claimsPrincipal"></param>
    /// <param name="claimtype"></param>
    /// <returns></returns>
    public static List<string>? Claims(this ClaimsPrincipal claimsPrincipal, string claimtype)
    {
        return claimsPrincipal?.FindAll(claimtype)?.Select(x => x.Value).ToList();
    }

    /// <summary>
    /// Kullanıcı rollerini tek bir satırda bize döner.
    /// </summary>
    /// <param name="claimsPrincipal"></param>
    /// <returns></returns>
    public static List<string>? ClaimRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.Claims(ClaimTypes.Role);
    }
}