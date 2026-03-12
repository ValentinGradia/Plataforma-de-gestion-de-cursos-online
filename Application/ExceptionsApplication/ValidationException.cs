using PlataformaDeGestionDeCursosOnline.Application.Behaviors;

namespace PlataformaDeGestionDeCursosOnline.Application.ExceptionsApplication;

public class ValidationException : Exception
{
    public IEnumerable<ValidationError> Errors { get; }
    
    public ValidationException(IEnumerable<ValidationError> validationErrors)
    {
        Errors = validationErrors;
    }
}