using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;

public record ActualizarTemarioCommand(Guid IdCurso, string Temario) : ICommand<Result>;
