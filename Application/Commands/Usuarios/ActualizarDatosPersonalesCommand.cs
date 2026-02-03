using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Usuarios;

public record ActualizarDatosPersonalesCommand(
    Guid IdEstudiante,
    string Nombre,
    string Apellido
) : ICommand<Result>;