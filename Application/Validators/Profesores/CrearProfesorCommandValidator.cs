using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Profesores;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Profesores;

public class CrearProfesorCommandValidator : AbstractValidator<CrearProfesorCommand>
{
    public CrearProfesorCommandValidator()
    {
        RuleFor(x => x.Pais)
            .DebeSerStringValido("Pais");

        RuleFor(x => x.Ciudad)
            .DebeSerStringValido("Ciudad");

        RuleFor(x => x.Calle)
            .DebeSerStringValido("Calle");

        RuleFor(x => x.Altura)
            .NotNull().WithMessage("Altura no puede ser nula.")
            .GreaterThan(0).WithMessage("Altura debe ser un número entero mayor a 0.");

        RuleFor(x => x.Email)
            .DebeSerStringValido("Email")
            .EmailAddress().WithMessage("Email no tiene un formato válido.");

        RuleFor(x => x.Contraseña)
            .DebeSerStringValido("Contraseña")
            .MinimumLength(8).WithMessage("Contraseña debe tener al menos 8 caracteres.");

        RuleFor(x => x.Dni)
            .DebeSerStringValido("Dni");

        RuleFor(x => x.Nombre)
            .DebeSerStringValido("Nombre");

        RuleFor(x => x.Apellido)
            .DebeSerStringValido("Apellido");

        RuleFor(x => x.Especialidad)
            .DebeSerStringValido("Especialidad");
    }
}

