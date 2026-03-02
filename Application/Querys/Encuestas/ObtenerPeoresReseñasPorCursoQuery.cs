using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Encuestas;

public record ObtenerPeoresReseñasPorCursoQuery(Guid IdCurso, int CantidadEncuestasAObtener) : IQuery<Result>;
