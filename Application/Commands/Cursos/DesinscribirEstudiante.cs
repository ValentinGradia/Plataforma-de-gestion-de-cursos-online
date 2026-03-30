using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;

public record DesinscribirEstudiante(Guid IdEstudiante, Guid IdCurso) : ICommand<Result>;