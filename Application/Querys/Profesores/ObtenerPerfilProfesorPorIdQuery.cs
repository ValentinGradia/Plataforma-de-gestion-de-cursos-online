using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Profesores;

public record ObtenerPerfilProfesorPorIdQuery(Guid IdProfesor) : IQuery<Result>;
