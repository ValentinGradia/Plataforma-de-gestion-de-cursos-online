using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Encuestas;

public record ObtenerMejoresReseñasPorCursoQuery(Guid IdCurso, int CantidadEncuestasAObtener) : IQuery<Result>;
