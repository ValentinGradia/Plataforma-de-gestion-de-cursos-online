using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;

public sealed record NotaCargada(Guid IdExamenEntregado, DateTime fechaNotaCargada) : IDomainEvent
{
    
}