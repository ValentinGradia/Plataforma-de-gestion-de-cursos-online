using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Application.Exceptions;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Usuarios;

internal class ActualizarDireccionCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork _unitOfWork) : ICommandHandler<ActualizarDireccionCommand, Result>
{
    public async Task<Result> Handle(ActualizarDireccionCommand request, CancellationToken cancellationToken)
    {
        Usuario usuario = await usuarioRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
        
        usuario!.CambiarPais(request.Pais);
        usuario.CambiarCiudad(request.Ciudad);
        usuario.CambiarCalle(request.Calle);
        usuario.CambiarAltura(request.Altura);

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}

