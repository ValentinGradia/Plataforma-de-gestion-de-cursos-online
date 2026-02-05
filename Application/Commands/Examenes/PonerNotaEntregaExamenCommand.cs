using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;

public record PonerNotaEntregaExamenCommand(Guid IdEntregaExamen, decimal NuevaNota) : ICommand<Result>;