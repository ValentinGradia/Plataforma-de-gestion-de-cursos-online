using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class DarPresenteCommandHandler : ICommandHandler<DarPresenteCommand, Result>
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEstudianteRepository _estudianteRepository;
    
    public DarPresenteCommandHandler(
        ICursoRepository cursoRepository,
        IUnitOfWork unitOfWork,
        IEstudianteRepository estudianteRepository)
    {
        _cursoRepository = cursoRepository;
        _unitOfWork = unitOfWork;
        _estudianteRepository = estudianteRepository;
    }
    
    public async Task<Result> Handle(DarPresenteCommand request, CancellationToken cancellationToken)
    {
        Task<Estudiante> TaskEstudiante = this._estudianteRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
        Task<Curso> TaskCurso = this._cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);

        Estudiante user = await TaskEstudiante;
        Curso curso = await TaskCurso;
        

        try
        {
            Clase clase = curso.ObtenerClase(request.IdClase);
            
            clase.DarPresente(request.IdEstudiante);
        }
        catch (AsistenciaYaCargadaException e)
        {
            return Result.Failure(e);
        }
        
        await this._unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}