using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class DesinscribirEstudianteCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository, 
    IInscripcionService inscripcionService, IEstudianteRepository _estudianteRepository) : ICommandHandler<DesinscribirEstudiante, Result>
{
    public async Task<Result> Handle(DesinscribirEstudiante request, CancellationToken cancellationToken)
    {
        Curso? curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        if (curso is null)
            return Result.Failure(new ArgumentException("No se encontró el curso con el ID proporcionado."));

        Estudiante? estudiante = await _estudianteRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
        if (estudiante is null)
            return Result.Failure(new ArgumentException("No se encontró el estudiante con el ID proporcionado."));

        Inscripcion? inscripcion = await cursoRepository.ObtenerInscripcionPorIdEstudianteYCursoAsync(
            request.IdEstudiante,
            request.IdCurso,
            cancellationToken);

        if (inscripcion is null)
            return Result.Failure(new ArgumentException("No se encontró una inscripción activa para el estudiante en el curso."));

        try
        {
            estudiante.DesinscribirDeCurso(request.IdCurso);
            inscripcionService.DesinscribirEstudiante(inscripcion, curso);
            await _estudianteRepository.ActualizarCursosActivosAsync(estudiante, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (InvalidOperationException e)
        {
            return Result.Failure(e);
        }
    }
}