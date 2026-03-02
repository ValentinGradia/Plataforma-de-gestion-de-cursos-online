using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Inscripciones;

public record ObtenerPorcentajeDeAsistenciaPorCursoQuery(Guid IdCurso, Guid IdInscripcion) : IQuery<Result>;
