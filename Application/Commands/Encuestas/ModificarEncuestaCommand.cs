using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Encuestas;

public record ModificarEncuestaCommand(Guid IdEncuesta,
    Guid IdEstudiante,
    int CalificacionCurso,
    int CalificacionMaterial,
    int CalificacionDocente,
    string Comentarios
) : ICommand<Result>;