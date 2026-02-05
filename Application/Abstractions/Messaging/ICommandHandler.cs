using System.Windows.Input;
using MediatR;

namespace PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

//el componente que va a recibir los comandos
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
where TCommand : ICommand
{    
    
}

//Para comandos que devuelven un valor
//El TCommand seria el comando que maneja el handler, y el TResponse es el tipo de dato que devuelve
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
where TCommand : ICommand<TResponse>
{    
    
}