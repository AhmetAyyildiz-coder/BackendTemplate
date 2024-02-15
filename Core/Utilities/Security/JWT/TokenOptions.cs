namespace Core.Utilities.Security.JWT;

/// <summary>
/// For options pattern
/// </summary>
public sealed class TokenOptions
{
    public string Auidience { get; set; }
    public string Issuer { get; set; }
    public int AccessTokenExpiration { get; set; }
    public string SecurityKey { get; set; }
}