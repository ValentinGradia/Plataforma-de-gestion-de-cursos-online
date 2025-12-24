using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;

public record InicarCursoCommand(Guid IdCurso) : ICommand<Result>;
