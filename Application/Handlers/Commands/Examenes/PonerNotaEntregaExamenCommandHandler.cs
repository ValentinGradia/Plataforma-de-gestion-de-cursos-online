using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Examenes;

internal class PonerNotaEntregaExamenCommandHandler : ICommandHandler<PonerNotaEntregaExamenCommand, Result>
{
    private IUnitOfWork _unitOfWork;
    private IEntregasExamenes _entregasExamenes;

    public PonerNotaEntregaExamenCommandHandler(IUnitOfWork unitOfWork, IEntregasExamenes _entregasExamenes)
    {
        this._unitOfWork = unitOfWork;
        this._entregasExamenes = _entregasExamenes;
    }
    
    public async Task<Result> Handle(PonerNotaEntregaExamenCommand request, CancellationToken cancellationToken)
    {
        EntregaDelExamen entrega = await this._entregasExamenes.ObtenerPorIdAsync(request.IdEntregaExamen);
        
        entrega.AsignarNota(request.NuevaNota);

        this._unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}