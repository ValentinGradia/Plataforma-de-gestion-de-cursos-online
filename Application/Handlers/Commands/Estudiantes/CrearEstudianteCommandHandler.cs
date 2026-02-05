using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Estudiantes;

internal class CrearEstudianteCommandHandler : ICommandHandler<CrearEstudianteCommand, Guid>
{
    private readonly IEstudianteRepository _estudianteRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CrearEstudianteCommandHandler(IEstudianteRepository estudianteRepository, IUnitOfWork unitOfWork)
    {
        _estudianteRepository = estudianteRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> Handle(CrearEstudianteCommand request, CancellationToken cancellationToken)
    {
        Estudiante estudiante = new Estudiante(
            request.Pais,
            request.Ciudad,
            request.Calle,
            request.Altura,
            request.Email,
            request.Contraseña,
            request.Dni,
            request.Nombre,
            request.Apellido
        );
        
        await this._estudianteRepository.GuardarAsync(estudiante);
        await this._unitOfWork.SaveChangesAsync();
        return estudiante.Id;
    }
}