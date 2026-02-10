using MediatR;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Email;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Examenes.DomainEvents;

internal class NotaCargadaDomainEventHandler(ICursoRepository cursoRepository, IEmailService emailService, IEstudianteRepository estudianteRepository): INotificationHandler<NotaCargada>
{
    public async Task Handle(NotaCargada notification, CancellationToken cancellationToken)
    {
        Curso curso = await cursoRepository.ObtenerCursoPorIdModeloExamen(notification.IdModeloExamen, cancellationToken);
        EntregaDelExamen entrega = curso.ObtenerInscripcionPorId(notification.IdInscripcionEstudiante).ObtenerHistorialDeEntregas().
            ToList().FirstOrDefault(e => e.Id == notification.IdExamenEntregado);
        
        Guid estudianteId = curso.ObtenerInscripcionPorId(notification.IdInscripcionEstudiante).IdEstudiante;
        Estudiante estudiante = await estudianteRepository.ObtenerPorIdAsync(estudianteId, cancellationToken);
        
        await emailService.EnviarEmailAsync(
            estudiante.Email,
            $"Nota cargada",
            $"{estudiante.Nombre}, tu nota para el examen {entrega.Id} siendo {entrega.Tipo} del curso" +
            $" {curso.Nombre} ha sido cargada. " + cancellationToken
        );
        
        
        
    }
}