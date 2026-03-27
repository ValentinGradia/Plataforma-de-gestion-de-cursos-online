using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Clases;

public record ObtenerConsultasDeClaseQuery(Guid IdClase) : IQuery<Result>;