using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;

public record CrearEntregaExamenCommand(Guid IdExamen, Guid IdEstudiante,
    string Respuesta, TipoExamen tipo, DateTime FechaLimite) : ICommand<Guid>;