using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;

public record CrearModeloExamenCommand(Guid IdCurso, string TemaExamen,
    TipoExamen Tipo, DateTime FechaLimite) : ICommand<Guid>;