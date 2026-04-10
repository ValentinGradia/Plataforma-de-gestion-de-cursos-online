using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class FinalizarCursoCommandHandler(
    IUnitOfWork unitOfWork,
    ICursoRepository cursoRepository,
    IEstudianteRepository estudianteRepository,
    IProfesorRepository profesorRepository
) : ICommandHandler<FinalizarCursoCommand, Result>
{
    public async Task<Result> Handle(FinalizarCursoCommand request, CancellationToken cancellationToken)
    {
        // 1. Obtener el curso
        Curso? curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        if (curso is null)
            return Result.Failure(new Exception("No se encontró el curso con el ID proporcionado."));
        

        // 2. Para cada inscripcion: mover el inscripcion a inactiva 
        foreach (Inscripcion inscripcion in curso.Inscripciones)
        {
            await estudianteRepository.ActualizarCursosActivosAInactivosAsync(inscripcion.IdCurso, cancellationToken);
        }

        // 3. Obtener el profesor y eliminar el curso de su lista de cursos a cargo
        Profesor? profesor = await profesorRepository.ObtenerPorIdAsync(curso.IdProfesor, cancellationToken);

        profesor.EliminarCursoACargo(request.IdCurso);
        
        // 5. Finalizar el curso
        curso.FinalizarCurso();

        await cursoRepository.ActualizarAsync(curso, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}