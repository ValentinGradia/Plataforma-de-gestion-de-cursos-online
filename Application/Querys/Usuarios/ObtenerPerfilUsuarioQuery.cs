using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Usuarios;

public record ObtenerPerfilUsuarioQuery(Guid IdUsuario) : IQuery<Result>;
