using MediatR;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;

public record SubirEntregaExamenCommand(Guid idExamen, Guid idEstudiante, 
    TipoExamen tipo, string respuesta, DateTime fechaLimite) : ICommand<Result>;