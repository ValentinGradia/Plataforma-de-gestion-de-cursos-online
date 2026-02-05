using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Inscripciones;

public record ObtenerEntregasDeCursadaQuery(Guid IdInscripcion) : IQuery<List<EntregaDelExamen>>;