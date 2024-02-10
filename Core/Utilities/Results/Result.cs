namespace Core.Utilities.Results;

public class Result : IResult
{
    public Result(bool success, string message): this(success)
    {
        Message = message;
    }

    public Result(bool succes)
    {
        this.Success = succes;
        this.Message = "";
    }

    public bool Success { get; }
    public string Message { get; }
}