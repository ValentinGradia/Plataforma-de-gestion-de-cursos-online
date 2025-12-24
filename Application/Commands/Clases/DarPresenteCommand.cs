using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;

public record DarPresenteCommand(Guid IdClase, Guid IdEstudiante) : ICommand<Result>;