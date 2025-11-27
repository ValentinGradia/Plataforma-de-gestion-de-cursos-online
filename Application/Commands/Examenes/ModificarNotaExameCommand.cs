using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;

public record ModificarNotaExameCommand(Guid IdEntregaExamen, double NuevaNota) : ICommand;