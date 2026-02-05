using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Examenes;

internal class SubirEntregaExamenCommandHandler : ICommandHandler<SubirEntregaExamenCommand, Result>
{
    public SubirEntregaExamenCommandHandler(IUnitOfWork unitOfWork, IEntregasDeExamenes entregasDeExamenes, IInscripcionRepository inscripcionRepository)
    {
        _unitOfWork = unitOfWork;
        _entregasDeExamenes = entregasDeExamenes;
        _inscripcionRepository = inscripcionRepository;
    }

    private readonly IUnitOfWork _unitOfWork;
    private readonly IEntregasDeExamenes _entregasDeExamenes;
    private readonly IInscripcionRepository _inscripcionRepository;
    
    public async Task<Result> Handle(SubirEntregaExamenCommand request, CancellationToken cancellationToken)
    {
        // EntregaDelExamen entrega = await this._entregasDeExamenes.ObtenerPorIdAsync(request.IdEntregaExamen);

        return Result.Success();
    }
}