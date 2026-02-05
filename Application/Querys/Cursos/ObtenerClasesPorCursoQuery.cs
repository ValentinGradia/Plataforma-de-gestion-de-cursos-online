using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Cursos;

public record ObtenerClasesPorCursoQuery(Guid IdCurso) : IQuery<List<Clase>>;