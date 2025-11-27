using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;

public record SubirEntregaExamenCommand(Guid IdEntregaExamen) : ICommand;