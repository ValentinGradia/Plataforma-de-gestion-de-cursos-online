using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class ReprogramarFechaDeClaseCommandHandler : ICommandHandler<ReprogramarFechaDeClaseCommand>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaseRepository _claseRepository;
    
    public ReprogramarFechaDeClaseCommandHandler(IUnitOfWork unitOfWork, IClaseRepository claseRepository)
    {
        _unitOfWork = unitOfWork;
        _claseRepository = claseRepository;
    }
    
    public async Task Handle(ReprogramarFechaDeClaseCommand request, CancellationToken cancellationToken)
    {
        Clase clase = await this._claseRepository.ObtenerPorIdAsync(request.IdClase, cancellationToken);
        
        if (clase is null)
        {
            throw new NotFoundException();
        }
        
        clase.ReprogramarClase(request.NuevaFecha);
    }
}