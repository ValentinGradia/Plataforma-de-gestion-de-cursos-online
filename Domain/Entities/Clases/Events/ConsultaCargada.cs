using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Events;

public record ConsultaCargada(Guid IdClase,Guid IdUsuarioQueCargoLaConsulta ,DateTime fechaConsulta) : IDomainEvent;