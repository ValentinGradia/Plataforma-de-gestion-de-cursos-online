using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;

public record ReprogramarFechaDeClaseCommand(Guid IdClase, DateTime NuevaFecha) : ICommand;