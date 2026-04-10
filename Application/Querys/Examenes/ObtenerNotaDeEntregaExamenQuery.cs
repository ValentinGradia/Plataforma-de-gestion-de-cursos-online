using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Examenes;

public record ObtenerNotaDeEntregaExamenQuery(Guid IdCurso, Guid IdEntregaExamen) : IQuery<Result>;
