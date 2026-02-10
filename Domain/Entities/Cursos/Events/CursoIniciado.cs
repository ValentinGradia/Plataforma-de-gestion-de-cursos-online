using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;

public record CursoIniciado(Guid IdCurso, DateTime FechaInicio) : IDomainEvent
{
    
}