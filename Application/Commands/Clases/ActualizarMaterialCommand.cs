using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;

public record ActualizarMaterialCommand(Guid IdClase, string NuevoMaterial) : ICommand<Result>;
