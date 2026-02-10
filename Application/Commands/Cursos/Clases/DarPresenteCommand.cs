using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;

public record DarPresenteCommand(Guid IdCurso, Guid IdClase, Guid IdInscripcionEstudiante) : ICommand<Result>;
