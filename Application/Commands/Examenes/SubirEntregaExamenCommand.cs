using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;

public record SubirEntregaExamenCommand(Guid IdExamen, Guid IdEstudiante,
    string Respuesta, TipoExamen Tipo, DateTime FechaLimite) : ICommand<Guid>;