using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class CrearClaseCommandHandler : ICommandHandler<CrearClaseCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaseRepository _claseRepository;

    public CrearClaseCommandHandler(IUnitOfWork unitOfWork, IClaseRepository claseRepository)
    {
        this._unitOfWork = unitOfWork;
        this._claseRepository = claseRepository;
    }
    
    public Task<Guid> Handle(CrearClaseCommand request, CancellationToken cancellationToken)
    {
        Clase clase = Clase.CrearClase(
            request.IdCurso,
            request.Material
        );
        
        this._claseRepository.GuardarAsync(clase);
        this._unitOfWork.SaveChangesAsync();
        return Task.FromResult(clase.Id);
    }
}