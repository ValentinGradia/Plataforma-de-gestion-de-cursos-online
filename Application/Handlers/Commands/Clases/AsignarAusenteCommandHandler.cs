using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class AsignarAusenteCommandHandler : ICommandHandler<AsignarAusenteCommand>
{
    private readonly IEstudianteRepository _estudianteRepository;
    private readonly IClaseRepository _claseRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AsignarAusenteCommandHandler(IEstudianteRepository estudianteRepository, IClaseRepository claseRepository, IUnitOfWork unitOfWork)
    {
        _estudianteRepository = estudianteRepository;
        _claseRepository = claseRepository;
        _unitOfWork = unitOfWork;
    }

    
    public async Task Handle(AsignarAusenteCommand request, CancellationToken cancellationToken)
    {
        Task<Estudiante> TaskEstudiante = this._estudianteRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
        Task<Clase> TaskClase = this._claseRepository.ObtenerPorIdAsync(request.IdClase, cancellationToken);
        
        Estudiante user = await TaskEstudiante;
        Clase clase = await TaskClase;
        
        if (user is null || clase is null)
        {
            throw new NotFoundException();
        }
        
        if (!(this._claseRepository.EstudiantePerteneceAClase(user.Id, clase.Id)))
        {
            throw new EstudianteNoPerteneceAlCurso();
        }
        
        clase.DarAusente(user.Id);
    }
}