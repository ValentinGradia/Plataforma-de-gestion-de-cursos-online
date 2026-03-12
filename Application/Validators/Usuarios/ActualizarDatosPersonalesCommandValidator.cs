using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Usuarios;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Usuarios;

public class ActualizarDatosPersonalesCommandValidator : AbstractValidator<ActualizarDatosPersonalesCommand>
{
    public ActualizarDatosPersonalesCommandValidator()
    {
        RuleFor(x => x.IdEstudiante)
            .DebeSerGuidValido("IdEstudiante");

        RuleFor(x => x.Nombre)
            .DebeSerStringValido("Nombre");

        RuleFor(x => x.Apellido)
            .DebeSerStringValido("Apellido");
    }
}

