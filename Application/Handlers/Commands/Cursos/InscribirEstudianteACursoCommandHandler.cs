using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Interfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class InscribirEstudianteACursoCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository, 
    IInscripcionService inscripcionService) : ICommandHandler<InscribirEstudianteACursoCommand, Result>
{
    public async Task<Result> Handle(InscribirEstudianteACursoCommand request, CancellationToken cancellationToken)
    {
        Curso curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);

        Inscripcion inscripcion = Inscripcion.CrearInscripcion(request.IdEstudiante, request.IdCurso);
        inscripcionService.InscribirEstudiante(inscripcion, curso!);
        
        await cursoRepository.CrearInscripcionAsync(inscripcion, cancellationToken);

        await cursoRepository.ActualizarAsync(curso, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
    
}