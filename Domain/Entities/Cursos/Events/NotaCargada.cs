using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;

public sealed record NotaCargada(DateTime fechaNotaCargada) : IDomainEvent
{
    
}