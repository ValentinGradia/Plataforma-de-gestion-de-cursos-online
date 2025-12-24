using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;

public record ReprogramarFechaDeClaseCommand(Guid IdClase, DateTime NuevaFecha) : ICommand<Result>;
