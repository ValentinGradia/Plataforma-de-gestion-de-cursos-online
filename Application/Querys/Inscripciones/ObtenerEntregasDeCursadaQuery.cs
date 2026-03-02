using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Inscripciones;

public record ObtenerEntregasDeCursadaQuery(Guid IdCurso, Guid IdInscripcion) : IQuery<Result>;
