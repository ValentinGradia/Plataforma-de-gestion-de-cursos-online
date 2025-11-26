using System.Windows.Input;
using MediatR;

namespace PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

//el componente que va a recibir los comandos
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
where TCommand : ICommand
{    
    
}

//Para comandos que devuelven un valor
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
where TCommand : ICommand<TResponse>
{    
    
}