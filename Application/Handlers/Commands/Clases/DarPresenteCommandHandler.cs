using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class DarPresenteCommandHandler : ICommandHandler<DarPresenteCommand, Result>
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DarPresenteCommandHandler(
        ICursoRepository cursoRepository,
        IUnitOfWork unitOfWork)
    {
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result> Handle(DarPresenteCommand request, CancellationToken cancellationToken)
    {
        Task<Curso> TaskCurso = this._cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);

        Curso curso = await TaskCurso;
        try
        {
            curso.ValidarSiElEstudianteNoPerteneceAlCurso(request.IdInscripcionEstudiante);

            Clase clase = curso.ObtenerClase(request.IdClase);

            Asistencia asistenciaEstudiante = clase.DarPresente(request.IdInscripcionEstudiante);

            Inscripcion inscripcion = curso.ObtenerInscripcionPorId(request.IdInscripcionEstudiante);
            inscripcion.AgregarAsistencia(asistenciaEstudiante);

            await this._cursoRepository.ActualizarClaseAsync(clase, cancellationToken);
            await this._unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
        catch (AsistenciaYaCargadaException e)
        {
            return Result.Failure(e);
        }
        catch (EstudianteNoPerteneceAlCurso e)
        {
            return Result.Failure(e);
        }

    }
}