using MediatR;

namespace PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

//patron publish (envia los eventos) -> suscriber (captura los eventos)
public interface IDomainEvent : INotification
{
    
}