using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;

public record CambiarProfesorCommand(Guid IdProfesor, Guid IdCurso) : ICommand;