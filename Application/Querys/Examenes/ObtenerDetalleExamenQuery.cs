using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Examenes;
 
public record ObtenerDetalleExamenQuery(Guid IdCurso, Guid IdExamen) : IQuery<Result>;
