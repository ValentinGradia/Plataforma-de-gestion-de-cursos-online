using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class DarPresenteCommandHandler : ICommandHandler<DarPresenteCommand>
{
    private readonly IClaseRepository _claseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEstudianteRepository _estudianteRepository;
    
    public Task Handle(DarPresenteCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}