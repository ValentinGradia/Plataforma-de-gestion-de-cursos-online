using MediatR;
using PlataformaDeGestionDeCursosOnline.Application.Email;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos.DomainEvents;

internal class CursoIniciadoDomainEventHandler(ICursoRepository cursoRepository, IEmailService emailService,
    IEstudianteRepository estudianteRepository) : INotificationHandler<CursoIniciado>
{
    public async Task Handle(CursoIniciado notification, CancellationToken cancellationToken)
    {
        Curso curso = await cursoRepository.ObtenerPorIdAsync(notification.IdCurso);
        
        List<Guid> estudiantesIds = curso.Inscripciones.Select(i => i.IdEstudiante).ToList();   
        List<Estudiante> estudiantes = await estudianteRepository.ObtenerPorIdsAsync(estudiantesIds, cancellationToken);

        foreach (Estudiante estudiante in estudiantes)
        {
            await emailService.EnviarEmailAsync(
                estudiante.Email,
                $"Curso iniciado en {curso.Nombre}",
                $"{estudiante.Nombre}, el curso ya inició"  +
                cancellationToken
            );
        }
    }
}