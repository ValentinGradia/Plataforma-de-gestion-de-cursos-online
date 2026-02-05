using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Cursos;

public record ObtenerInformacionCursoPorIdQuery(Guid IdCurso) : IQuery<CursoDTO>;