using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;

public record SubirCorrecionEntregaExamen(Guid IdEntregaExamen, double Nota, string comentario = null) : ICommand;