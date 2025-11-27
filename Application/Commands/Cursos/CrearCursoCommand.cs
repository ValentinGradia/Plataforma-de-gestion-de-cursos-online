using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;

namespace PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;

public record CrearCursoCommand(Profesor profesor, string temario, string nombreCurso, DateTime inicio, DateTime fin)
: ICommand;