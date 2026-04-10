using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

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

        EntregaDelExamen? entrega = await this._cursoRepository.ObtenerEntregaExamenPorIdAsync(request.IdEntregaExamen, cancellationToken);
        if (entrega is null)
            return Result.Failure(new Exception("No se encontro la entrega de examen."));

        await this._cursoRepository.ActualizarEntregaExamenAsync(entrega, cancellationToken);
        await this._unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}