using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Profesores;

public record ObtenerPerfilProfesorPorIdQuery(Guid IdProfesor) : IQuery<ProfesorDTO>;