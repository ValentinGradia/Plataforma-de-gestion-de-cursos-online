using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Inscripciones;

//La inscripcion se crea cuando el estudiante se inscribe en el curso
public record CrearInscripcionCommand(
    Guid IdEstudiante,
    Guid IdCurso
) : ICommand<Result>;