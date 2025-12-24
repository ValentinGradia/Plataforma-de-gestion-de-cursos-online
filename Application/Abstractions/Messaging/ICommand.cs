using MediatR;

namespace PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

//Por lo general en CQRS, los comandos no devuelven datos, solo indican que se debe
//realizar una accion
//El IRequest viene de MediatR y es el que va a permitir la comunicacion entre el query y su handler
public interface ICommand : IRequest
{
    
}

public interface ICommand<TResponse> : IRequest<TResponse>
{
    
}