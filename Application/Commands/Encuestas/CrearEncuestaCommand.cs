using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Encuestas;

public record CrearEncuestaCommand(
    Guid idCurso,
    Guid? idEstudiante,
    int calificacionCurso,
    int calificacionMaterial,
    int calificacionDocente,
    string comentarios
) : ICommand<Guid>;