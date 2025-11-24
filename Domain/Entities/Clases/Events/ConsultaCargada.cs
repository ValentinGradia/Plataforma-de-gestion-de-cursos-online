using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Events;

public record ConsultaCargada(Guid IdClase, DateTime fechaConsulta) : IDomainEvent;