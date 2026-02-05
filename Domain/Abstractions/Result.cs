namespace PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

//Creamos una clase Result para representar el resultado de una operación y mapear objetos tipo Exception
public class Result
{
    
    protected internal Result(bool isSuccess, Exception? exception = null)
    {
        IsSuccess = isSuccess;
        Exception = exception;
    }
    
    protected internal Result(bool isSuccess, Exception? exception = null, object? dato = null) : this(isSuccess, exception)
    {
        Data = dato;
    }

    
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public object? Data { get; }
    public Exception? Exception { get; }
    
    public static Result Success() => new Result(true, null);
    

    public static Result Failure(Exception exception) 
        => new Result(false, exception);
}