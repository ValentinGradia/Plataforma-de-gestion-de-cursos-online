using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;

public record CrearCursoCommand(Guid IdProfesor, string temario, string nombreCurso, DateTime inicio, DateTime fin) 
    : ICommand<Guid>;