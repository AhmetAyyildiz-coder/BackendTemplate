namespace Core.Utilities.Results;

/// <summary>
/// Yapılan işlem sonrası dönecek olan nesne 
/// </summary>
public interface IResult
{
    bool Success { get; }
    string Message { get; }
}