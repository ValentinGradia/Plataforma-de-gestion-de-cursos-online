using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos.Examenes;

public record PonerNotaEntregaExamenCommand(Guid IdEntregaExamen,Guid IdCurso,Guid IdProfesor,decimal NuevaNota) : ICommand<Result>;