using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Inscripciones;

public record ObtenerInformacionDeInscripcionQuery(Guid IdCurso, Guid IdInscripcion) : IQuery<Result>;


