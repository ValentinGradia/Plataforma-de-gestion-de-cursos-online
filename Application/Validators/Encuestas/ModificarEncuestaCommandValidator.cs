using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Encuestas;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Encuestas;

public class ModificarEncuestaCommandValidator : AbstractValidator<ModificarEncuestaCommand>
{
    public ModificarEncuestaCommandValidator()
    {
        RuleFor(x => x.IdEncuesta)
            .DebeSerGuidValido("IdEncuesta");

        RuleFor(x => x.IdEstudiante)
            .DebeSerGuidValido("IdEstudiante");

        RuleFor(x => x.CalificacionCurso)
            .NotNull().WithMessage("CalificacionCurso no puede ser nulo.")
            .InclusiveBetween(1, 10).WithMessage("CalificacionCurso debe estar entre 1 y 10.");

        RuleFor(x => x.CalificacionMaterial)
            .NotNull().WithMessage("CalificacionMaterial no puede ser nulo.")
            .InclusiveBetween(1, 10).WithMessage("CalificacionMaterial debe estar entre 1 y 10.");

        RuleFor(x => x.CalificacionDocente)
            .NotNull().WithMessage("CalificacionDocente no puede ser nulo.")
            .InclusiveBetween(1, 10).WithMessage("CalificacionDocente debe estar entre 1 y 10.");

        RuleFor(x => x.Comentarios)
            .DebeSerStringValido("Comentarios");
    }
}

