using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class DarPresenteCommandHandler : ICommandHandler<DarPresenteCommand, Result>
{
    private readonly IClaseRepository _claseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEstudianteRepository _estudianteRepository;
    
    public async Task<Result> Handle(DarPresenteCommand request, CancellationToken cancellationToken)
    {
        Task<Estudiante> TaskEstudiante = this._estudianteRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
        Task<Clase> TaskClase = this._claseRepository.ObtenerPorIdAsync(request.IdClase, cancellationToken);

        Estudiante user = await TaskEstudiante;
        Clase clase = await TaskClase;
        
        if (user is null || clase is null)
        {
            throw new NotFoundException();
        }

        try
        {
            clase.DarPresente(request.IdEstudiante);
        }
        catch (AsistenciaYaCargadaException e)
        {
            return Result.Failure(e);
        }
        
        await this._unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}