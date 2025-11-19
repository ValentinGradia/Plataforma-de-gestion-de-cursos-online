using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;

public sealed record UsuarioInscriptoEnCurso (Guid IdUsuarioInscripto, DateTime FechaInscripto) : IDomainEvent
{

}