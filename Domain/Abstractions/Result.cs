namespace PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

//Creamos una clase Result para representar el resultado de una operación y mapear objetos tipo Exception
public class Result
{
    private Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    private Result(bool isSuccess, Exception? exception): this(isSuccess)
    {
        Exception = exception;
    }

    
    public Result(bool isSuccess, object? dato = null) : this(isSuccess)
    {
        Data = dato;
    }

    private Result(bool isSuccess, string errorMessage, Exception? exception = null) : this(isSuccess, exception)
    {
        this.ErrorMessage = errorMessage;
    }


    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public object? Data { get; }
    public Exception? Exception { get; }
    public string ErrorMessage { get; }
    
    public static Result Success() => new Result(true, null);
    

    public static Result Failure(Exception exception) 
        => new Result(false, exception);
    
    public static Result Failure(Exception e, string errorMessage) 
        => new Result(false, errorMessage,e);
}