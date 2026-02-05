using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Clases;

public record ObtenerAsistenciasDeClase(Guid IdClase) : IQuery<IReadOnlyCollection<Asistencia>>;