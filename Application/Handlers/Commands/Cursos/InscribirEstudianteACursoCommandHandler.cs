using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class InscribirEstudianteACursoCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository, 
    IInscripcionService inscripcionService, IEstudianteRepository _estudianteRepository) : ICommandHandler<InscribirEstudianteACursoCommand, Result>
{
    public async Task<Result> Handle(InscribirEstudianteACursoCommand request, CancellationToken cancellationToken)
    {
        Curso curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        
        Estudiante estudiante = await _estudianteRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
        estudiante.InscribirEnCurso(request.IdCurso);
        
        Inscripcion inscripcion = Inscripcion.CrearInscripcion(request.IdEstudiante, request.IdCurso);
        inscripcionService.InscribirEstudiante(inscripcion, curso!);

        // Persistir el nuevo curso activo en la tabla EstudianteCursosActivos
        await _estudianteRepository.ActualizarCursosActivosAsync(estudiante, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
    
}