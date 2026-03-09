using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class DarDeBajaEstudianteCommandHandler (IEstudianteRepository estudianteRepository, 
    ICursoRepository cursoRepository, 
    IUnitOfWork _unitOfWork) : ICommandHandler<DarDeBajaEstudianteCommand, Result>
{
    public async Task<Result> Handle(DarDeBajaEstudianteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Curso curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
            curso.DarseDeBajaEstudiante(request.IdEstudiante);

            // Reflejar en BD que el curso ya no está en los activos del estudiante
            Estudiante estudiante = await estudianteRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
            estudiante.CompletarCurso(request.IdCurso);
            await estudianteRepository.ActualizarCursosActivosAsync(estudiante, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (ArgumentNullException e)
        {
            return Result.Failure(e,e.Message);
        }
    }
}