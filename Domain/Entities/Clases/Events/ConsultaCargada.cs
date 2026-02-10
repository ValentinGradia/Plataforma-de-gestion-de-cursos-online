using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Events;

public record ConsultaCargada(Guid IdCurso, Guid IdUsuarioQueCargoLaConsulta ,DateTime fechaConsulta) : IDomainEvent;