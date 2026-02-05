using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Examenes;

internal class CrearEntregaExamenCommandHandler 
    : ICommandHandler<CrearEntregaExamenCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEntregasDeExamenes _entregaExamenRepository;

    public CrearEntregaExamenCommandHandler(
        IUnitOfWork unitOfWork,
        IEntregasDeExamenes entregaExamenRepository)
    {
        _unitOfWork = unitOfWork;
        _entregaExamenRepository = entregaExamenRepository;
    }

    public async Task<Guid> Handle(
        CrearEntregaExamenCommand request,
        CancellationToken cancellationToken)
    {
        EntregaDelExamen entregaExamen = EntregaDelExamen.Crear(
            request.IdExamen,
            request.IdEstudiante,
            request.tipo,
            request.Respuesta,
            request.FechaLimite
        );

        await _entregaExamenRepository.GuardarAsync(entregaExamen);
        await _unitOfWork.SaveChangesAsync();

        return entregaExamen.Id;
    }
}
