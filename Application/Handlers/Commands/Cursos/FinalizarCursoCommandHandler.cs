using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class FinalizarCursoCommandHandler(
    IUnitOfWork unitOfWork,
    ICursoRepository cursoRepository,
    IEstudianteRepository estudianteRepository
) : ICommandHandler<FinalizarCursoCommand, Result>
{
    public async Task<Result> Handle(FinalizarCursoCommand request, CancellationToken cancellationToken)
    {
        // 1. Obtener el curso
        Curso? curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        if (curso is null)
            return Result.Failure(new Exception("No se encontró el curso con el ID proporcionado."));

        // 2. Obtener todos los estudiantes inscritos en el curso
        List<Estudiante> estudiantes = await cursoRepository.ObtenerEstudiantesInscriptosEnCurso(request.IdCurso, cancellationToken);

        // 3. Para cada estudiante: mover el curso de inscritosActualmente -> historialDeCursos
        foreach (Estudiante estudiante in estudiantes)
        {
            estudiante.CompletarCurso(request.IdCurso);

            // Sincronizar ambas tablas en BD
            await estudianteRepository.ActualizarCursosActivosAsync(estudiante, cancellationToken);
            await estudianteRepository.ActualizarHistorialCursosAsync(estudiante, cancellationToken);
        }

        // 4. Finalizar el curso
        curso.FinalizarCurso();

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}