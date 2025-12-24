using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;

public record SubirEncuestaCommand(Guid IdCurso,
            Guid idEstudiante,
            int calificacionCurso,
            int calificacionMaterial,
            int calificacionDocente,
            string comentarios) : ICommand<Result>;
            