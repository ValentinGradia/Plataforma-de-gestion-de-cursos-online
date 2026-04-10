using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Profesores;

internal class CrearProfesorCommandHandler : ICommandHandler<CrearProfesorCommand, Guid>
{
    private readonly IProfesorRepository _profesorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CrearProfesorCommandHandler(IProfesorRepository profesorRepository, IUnitOfWork unitOfWork)
    {
        this._profesorRepository = profesorRepository;
        this._unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> Handle(CrearProfesorCommand request, CancellationToken cancellationToken)
    {
        Profesor profesor = Profesor.CrearProfesor(
        request.Pais,
        request.Ciudad,
        request.Calle,
        request.Altura,
        request.Email,
        request.Contraseña,
        request.Dni,
        request.Nombre,
        request.Apellido,
        request.Especialidad
        );

        //Aca EF no trackea la entidad por lo que tenemos que guardarla explicitamente
        await this._profesorRepository.GuardarAsync(profesor);
        await this._unitOfWork.SaveChangesAsync();
        return profesor.Id;
    }
}