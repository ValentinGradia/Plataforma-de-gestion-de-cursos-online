using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;

public record DarPresenteCommand(Guid IdClase, Guid IdEstudiante) : ICommand;