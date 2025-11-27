using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;

public record DarDeBajaEstudiante(Guid IdEstudiante, Guid IdCurso) : ICommand;