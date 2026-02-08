using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;

public record CambiarProfesorCommand(Guid IdProfesor, Guid IdCurso) : ICommand<Result>;
