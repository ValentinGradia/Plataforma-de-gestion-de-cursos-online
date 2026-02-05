using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Estudiantes;

public record CrearEstudianteCommand(
    string Pais,
    string Ciudad,
    string Calle,
    int Altura,
    string Email,
    string Contraseña,
    string Dni,
    string Nombre,
    string Apellido
) : ICommand<Guid>;