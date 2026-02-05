using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Clases;

public record ObtenerConsultasDeClaseQuery(Guid IdClase) : IQuery<List<Consulta>>;