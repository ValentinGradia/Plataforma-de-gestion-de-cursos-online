using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Usuarios;

internal class ActualizarContactoCommandHandler(IUsuarioRepository usuarioRepository, IUnitOfWork _unitOfWork ) : ICommandHandler<ActualizarContactoCommand, Result>
{

    public async Task<Result> Handle(ActualizarContactoCommand request, CancellationToken cancellationToken)
    {
        Usuario usuario =  await usuarioRepository.ObtenerPorIdAsync(request.IdUsuario);
        
        usuario.CambiarEmail(request.Email);
        
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();

    }
}