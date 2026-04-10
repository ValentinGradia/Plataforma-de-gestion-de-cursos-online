using MediatR;
using PlataformaDeGestionDeCursosOnline.Application.Email;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos.DomainEvents;

internal class ExamenSubidoDomainEventHandler (ICursoRepository cursoRepository, IEstudianteRepository estudianteRepository,
    IEmailService emailService) : INotificationHandler<ExamenSubido>
{
    public async Task Handle(ExamenSubido notification, CancellationToken cancellationToken)
    {
        Curso curso = await cursoRepository.ObtenerPorIdAsync(notification.IdCurso);
        
        List<Guid> estudiantesIds = curso.Inscripciones.Select(i => i.IdEstudiante).ToList();   
        List<Estudiante> estudiantes = await estudianteRepository.ObtenerPorIdsAsync(estudiantesIds, cancellationToken);

        foreach (Estudiante estudiante in estudiantes)
        {
            await emailService.EnviarEmailAsync(
                estudiante.Email,
                $"Examen subido en {curso.Nombre}",
                $"{estudiante.Nombre}, ya se cargo el modelo de examen "  +
                cancellationToken
            );
        }
    }
}