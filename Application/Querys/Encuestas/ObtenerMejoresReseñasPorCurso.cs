using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Encuestas;

public record ObtenerMejoresReseñasPorCurso(Guid IdCurso, int CantidadEncuestasAObtener) : IQuery<List<Encuesta>>;