using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Cursos;

public record ObtenerClasesPorCursoQuery(Guid IdCurso) : IQuery<Result>;