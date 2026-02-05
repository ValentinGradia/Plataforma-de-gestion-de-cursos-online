using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;

public record SubirEntregaExamenCommand(Guid IdEntregaExamen, double Nota, string comentario = null) : ICommand<Result>;