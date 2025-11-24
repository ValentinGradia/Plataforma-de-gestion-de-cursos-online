using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Events;

public record ClaseIniciada(Guid IdClase, DateTime Inicio) : IDomainEvent;