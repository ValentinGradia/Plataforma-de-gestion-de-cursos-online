using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Examenes;

internal class PonerNotaEntregaExamenCommandHandler : ICommandHandler<PonerNotaEntregaExamenCommand, Result>
{
    private IUnitOfWork _unitOfWork;
    private ICursoRepository _cursoRepository;

    public PonerNotaEntregaExamenCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository)
    {
        this._unitOfWork = unitOfWork;
        this._cursoRepository = cursoRepository;
    }
    
    public async Task<Result> Handle(PonerNotaEntregaExamenCommand request, CancellationToken cancellationToken)
    {
        Curso curso = await this._cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
            
        curso.CargarCalificacionAEntregaDeExamen(request.IdEntregaExamen, request.IdProfesor, request.NuevaNota);

        this._unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}