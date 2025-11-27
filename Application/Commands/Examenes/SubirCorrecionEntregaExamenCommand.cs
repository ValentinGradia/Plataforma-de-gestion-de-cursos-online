using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;

public record SubirCorrecionEntregaExamenCommand(Guid IdEntregaExamen, double Nota, string comentario = null) : ICommand;