using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Examenes;

public record ObtenerDetalleEntregaExamenQuery(Guid IdCurso, Guid IdEntregaExamen) : IQuery<Result>;
