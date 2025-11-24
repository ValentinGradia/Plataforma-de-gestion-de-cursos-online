using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;

public sealed record CursoIniciado(Guid IdCurso, DateTime Inicio) : IDomainEvent;