using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Clases;

public record ObtenerInformacionClaseQuery(Guid IdClase) : IQuery<Result>;