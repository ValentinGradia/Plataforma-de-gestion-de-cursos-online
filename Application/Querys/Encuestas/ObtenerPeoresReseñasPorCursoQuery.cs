using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Encuestas;

public record ObtenerPeoresReseñasPorCursoQuery(Guid IdCurso, int CantidadEncuestasAObtener) : IQuery<List<Encuesta>>;