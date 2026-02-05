using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Profesores;
using PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Profesores;

internal class CrearProfesorCommandHandler : ICommandHandler<CrearProfesorCommand,Result>
{
    private readonly IProfesorRepository _profesorRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public async Task<Result> Handle(CrearProfesorCommand request, CancellationToken cancellationToken)
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
        return new Result(true,null,profesor.Id);
    }
}