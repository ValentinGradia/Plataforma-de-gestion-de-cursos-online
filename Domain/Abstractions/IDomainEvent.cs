using MediatR;

namespace PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

//patron publish (envia los eventos) -> suscriber (captura los eventos)
//Todos los domain events van a implementar esta interfaz que implementa INotification de MediatR, esto para que MediatR
//pueda conectar esa notificacion con su respectivo. El INotification es una interfaz vacia que sirve como marcador para las notificaciones en MediatR.
public interface IDomainEvent : INotification
{
    
}