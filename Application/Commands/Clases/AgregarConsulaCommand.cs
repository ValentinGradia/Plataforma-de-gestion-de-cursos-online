using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;

public record AgregarConsulaCommand(Guid IdUsuario, Guid IdClase, string Titulo, string Descripcion) : ICommand;