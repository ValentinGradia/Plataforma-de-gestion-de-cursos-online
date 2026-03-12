using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace PlataformaDeGestionDeCursosOnline.Application.Behaviors;

// El validation behavior se encarga de interceptar las solicitudes antes de que lleguen a los handlers y
// ejecutar la validación utilizando FluentValidation. Si la validación falla, se lanzará una excepción con los errores de validación.
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators; //Podemos inyectar una colección de validadores para el tipo de solicitud TRequest

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        
        var context = new ValidationContext<TRequest>(request); // Creamos un contexto de validación para la solicitud actual
        
        // Todas las validaciones de error
        var validationErrors = _validators
            .Select(validators => validators.Validate(context))
            .Where(validationresult =>  validationresult.Errors.Any())
            .SelectMany(validationResult => validationResult.Errors)
            .Select(failure => new ValidationError(
                PropertyName: failure.PropertyName,
                ErrorMessage: failure.ErrorMessage
            )).ToList();  //Esto es lo que tenemos que hacer para obtener todos los validations errors de un request de tipo command que tiene errores en la data
        //disparaondo estos errores.

        
        // Si hay errores de validación, lanzamos una excepción con los detalles de los errores
        if (validationErrors.Any())
        {
            throw new ExceptionsApplication.ValidationException(validationErrors);
        }

        return await next();
    }
}