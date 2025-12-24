namespace PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

public class Result
{
    protected internal Result(bool isSuccess, Exception? exception = null)
    {
        IsSuccess = isSuccess;
        Exception = exception;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Exception? Exception { get; }
    
    public static Result Success() => new Result(true, null);

    public static Result Failure(Exception exception) 
        => new Result(false, exception);
}