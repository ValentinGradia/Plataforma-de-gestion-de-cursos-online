namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Usuarios;

using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Application.Exceptions;

internal class ActualizarDatosPersonalesCommandHandler : ICommandHandler<ActualizarDatosPersonalesCommand, Result>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActualizarDatosPersonalesCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ActualizarDatosPersonalesCommand request, CancellationToken cancellationToken)
    {
        Usuario usuario = await _usuarioRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
        
        usuario!.CambiarNombre(request.Nombre);
        usuario!.CambiarApellido(request.Apellido);

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}