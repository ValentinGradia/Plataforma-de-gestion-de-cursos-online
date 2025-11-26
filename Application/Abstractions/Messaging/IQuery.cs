using MediatR;

namespace PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

//Este IQuery va a trabajar por todos los querys del proyecto, por cada lectura de dato.
public interface IQuery<TResponse> : IRequest<TResponse>
//trabaja para todas las clases del proyecto, por ende es generico donde TResponse es el tipo
//de dato que va a devolver el query
//El IRequest viene de MediatR y es el que va a permitir la comunicacion entre el query y su handler
{ 
    
}