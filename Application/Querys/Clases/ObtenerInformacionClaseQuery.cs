using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Clases;

public record ObtenerInformacionClaseQuery(Guid IdClase) : IQuery<ClaseDTO>;