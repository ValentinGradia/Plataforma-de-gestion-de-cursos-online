using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Usuarios;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Usuarios;

public class ActualizarDireccionCommandValidator : AbstractValidator<ActualizarDireccionCommand>
{
    public ActualizarDireccionCommandValidator()
    {
        RuleFor(x => x.IdEstudiante)
            .DebeSerGuidValido("IdEstudiante");

        RuleFor(x => x.Pais)
            .DebeSerStringValido("Pais");

        RuleFor(x => x.Ciudad)
            .DebeSerStringValido("Ciudad");

        RuleFor(x => x.Calle)
            .DebeSerStringValido("Calle");

        RuleFor(x => x.Altura)
            .NotNull().WithMessage("Altura no puede ser nula.")
            .GreaterThan(0).WithMessage("Altura debe ser un número entero mayor a 0.");
    }
}

