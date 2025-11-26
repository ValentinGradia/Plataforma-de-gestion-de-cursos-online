using System.Windows.Input;
using ICommand = PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging.ICommand;

namespace PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;

public record InscribirEstudianteACursoCommand(Guid IdEstudiante, Guid IdCurso) : ICommand;