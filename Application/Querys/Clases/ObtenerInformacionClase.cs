using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Clases;

public record ObtenerInformacionClase(Guid IdClase) : IQuery<Clase>;