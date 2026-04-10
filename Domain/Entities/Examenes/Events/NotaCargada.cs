using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.Events;

public sealed record NotaCargada(Guid IdExamenEntregado,Guid IdModeloExamen , Guid IdInscripcionEstudiante) : IDomainEvent
{
    
}