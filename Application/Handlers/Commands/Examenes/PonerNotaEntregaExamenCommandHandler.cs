using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Examenes;

internal class PonerNotaEntregaExamenCommandHandler : ICommandHandler<PonerNotaEntregaExamenCommand, Result>
{
    private IUnitOfWork _unitOfWork;
    private IEntregasDeExamenes _entregasDeExamenes;

    public PonerNotaEntregaExamenCommandHandler(IUnitOfWork unitOfWork, IEntregasDeExamenes entregasDeExamenes)
    {
        this._unitOfWork = unitOfWork;
        this._entregasDeExamenes = entregasDeExamenes;
    }
    
    public async Task<Result> Handle(PonerNotaEntregaExamenCommand request, CancellationToken cancellationToken)
    {
        EntregaDelExamen entrega = await this._entregasDeExamenes.ObtenerPorIdAsync(request.IdEntregaExamen);
        
        entrega.AsignarNota(request.NuevaNota);

        this._unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}