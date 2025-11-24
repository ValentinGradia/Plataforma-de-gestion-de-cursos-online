using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;

public sealed record CursoFinalizado(Guid IdCurso, DateTime fechaFinalizacion) : IDomainEvent;