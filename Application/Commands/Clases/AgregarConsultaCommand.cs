using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;

public record AgregarConsultaCommand(Guid IdEstudiante, Guid IdClase, string Titulo, string Descripcion) : ICommand<Result>;
