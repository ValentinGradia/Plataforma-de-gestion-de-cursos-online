using AutoMapper;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Usuarios;

internal class ObtenerPerfilUsuarioQueryHandler(IUsuarioRepository usuarioRepository, IMapper mapper) : IQueryHandler<ObtenerPerfilUsuarioQuery, Result>
{
    public async Task<Result> Handle(ObtenerPerfilUsuarioQuery request, CancellationToken cancellationToken)
    {
        Usuario? usuario = await usuarioRepository.ObtenerPorIdAsync(request.IdUsuario, cancellationToken);
        if (usuario is null)
            return Result.Failure(new ArgumentException("No se encontró el usuario con el ID proporcionado."));

        return new Result(true, mapper.Map<UsuarioDTO>(usuario));
    }
}
