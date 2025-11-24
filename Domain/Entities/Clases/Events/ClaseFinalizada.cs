using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Events;

public record ClaseFinalizada(Guid IdClase, DateTime Fin) : IDomainEvent;