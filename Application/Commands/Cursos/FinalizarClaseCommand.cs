using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;

public record FinalizarClaseCommand(Guid IdClase) : ICommand<Result>;
