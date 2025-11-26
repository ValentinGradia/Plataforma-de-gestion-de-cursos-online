using MediatR;

namespace PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

//Por lo general en CQRS, los comandos no devuelven datos, solo indican que se debe
//realizar una accion
public interface ICommand : IRequest
{
    
}

public interface ICommand<TResponse> : IRequest<TResponse>
{
    
}