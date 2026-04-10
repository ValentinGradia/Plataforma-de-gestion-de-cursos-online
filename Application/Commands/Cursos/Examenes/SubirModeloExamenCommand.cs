using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.Enums;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos.Examenes;

public record SubirModeloExamenCommand(Guid IdCurso, string TemaExamen,
    TipoExamen Tipo, DateTime FechaLimite) : ICommand<Guid>;