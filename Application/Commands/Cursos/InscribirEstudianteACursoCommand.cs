using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;

public record InscribirEstudianteACursoCommand(Guid IdEstudiante, Guid IdCurso) : ICommand<Result>;
