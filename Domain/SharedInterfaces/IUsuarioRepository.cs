using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<UsuarioDTO?> VerDatosUsuario(Guid id, CancellationToken cancellationToken = default);
}