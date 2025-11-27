using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;

public record ActualizarMaterialCommand(Guid IdClase, string NuevoMaterial) : ICommand;