using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Examenes;

public record ObtenerDetalleExamenQuery(Guid IdExamen) : IQuery<Examen>;