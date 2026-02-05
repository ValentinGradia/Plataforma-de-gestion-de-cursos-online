using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Encuestas;

public record ObtenerEncuestasPorCursoQuery(Guid IdCurso) : IQuery<List<Encuesta>>;