using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;

//Los que restantes sin poner presente, se asigna ausente automaticamente.
public record AsignarAusenteCommand(Guid IdClase, Guid IdEstudiante) : ICommand<Result>;
