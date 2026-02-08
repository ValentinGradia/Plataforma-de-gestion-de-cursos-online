using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class AsignarAusenteCommandHandler : ICommandHandler<AsignarAusenteCommand,Result>
{
    private readonly IEstudianteRepository _estudianteRepository;
    private readonly ICursoRepository _cursoRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AsignarAusenteCommandHandler(IEstudianteRepository estudianteRepository, ICursoRepository cursoRepository, IUnitOfWork unitOfWork)
    {
        _estudianteRepository = estudianteRepository;
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
    }

    
    public async Task<Result> Handle(AsignarAusenteCommand request, CancellationToken cancellationToken)
    {
        Task<Estudiante> TaskEstudiante = this._estudianteRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
        Task<Curso> TaskCurso = this._cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        
        Estudiante estudiante = await TaskEstudiante;
        Curso curso = await TaskCurso;
        
        Clase clase = curso.ObtenerClase(request.IdClase);

        try
        {
            curso.ValidarSiElEstudianteNoPerteneceAlCurso(estudiante.Id);
            
            clase.DarAusente(estudiante.Id);
            await this._unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
        catch (EstudianteNoPerteneceAlCurso e)
        {
            return Result.Failure(e);
        }
        
    }
}