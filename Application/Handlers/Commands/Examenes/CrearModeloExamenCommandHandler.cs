using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Examenes;

internal class CrearModeloExamenCommandHandler 
    : ICommandHandler<CrearModeloExamenCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExamenRepository _modeloExamenRepository;

    public CrearModeloExamenCommandHandler(
        IUnitOfWork unitOfWork,
        IExamenRepository modeloExamenRepository)
    {
        _unitOfWork = unitOfWork;
        _modeloExamenRepository = modeloExamenRepository;
    }

    public Task<Guid> Handle(
        CrearModeloExamenCommand request,
        CancellationToken cancellationToken)
    {
        Examen modeloExamen = Examen.CrearExamen(
            request.IdCurso,
            request.Tipo,
            request.TemaExamen,
            request.FechaLimite
        );

        _modeloExamenRepository.GuardarAsync(modeloExamen);
        _unitOfWork.SaveChangesAsync();

        return Task.FromResult(modeloExamen.Id);
    }
}
