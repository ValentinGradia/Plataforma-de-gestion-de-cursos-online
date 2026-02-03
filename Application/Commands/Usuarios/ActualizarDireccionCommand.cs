using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Usuarios;

public record ActualizarDireccionCommand
(
    Guid IdEstudiante,
    string Pais,
    string Ciudad,
    string Calle,
    int Altura
) : ICommand<Result>;