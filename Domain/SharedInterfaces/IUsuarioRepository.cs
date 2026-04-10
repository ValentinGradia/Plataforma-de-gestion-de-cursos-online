using PlataformaDeGestionDeCursosOnline.Domain.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios;

namespace PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<UsuarioDTO?> VerDatosUsuario(Guid id, CancellationToken cancellationToken = default);
}