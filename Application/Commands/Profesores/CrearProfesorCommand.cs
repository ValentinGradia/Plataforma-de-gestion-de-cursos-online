using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Profesores;

public record CrearProfesorCommand(
    string Pais,
    string Ciudad,
    string Calle,
    int Altura,
    string Email,
    string Contraseña,
    string Dni,
    string Nombre,
    string Apellido,
    string Especialidad
) : ICommand<Guid>;