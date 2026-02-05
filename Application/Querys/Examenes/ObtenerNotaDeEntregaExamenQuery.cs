using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Examenes;

public record ObtenerNotaDeEntregaExamenQuery(Guid IdEntregaExamen) : IQuery<Nota>;