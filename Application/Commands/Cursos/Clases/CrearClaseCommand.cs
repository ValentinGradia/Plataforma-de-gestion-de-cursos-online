using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos.Clases;

public record CrearClaseCommand(Guid IdCurso, string Material) : ICommand<Guid>;