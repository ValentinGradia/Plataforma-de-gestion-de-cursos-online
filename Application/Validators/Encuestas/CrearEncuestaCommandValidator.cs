using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Encuestas;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Encuestas;

public class CrearEncuestaCommandValidator : AbstractValidator<CrearEncuestaCommand>
{
    public CrearEncuestaCommandValidator()
    {
        RuleFor(x => x.idCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.calificacionCurso)
            .NotNull().WithMessage("CalificacionCurso no puede ser nulo.")
            .InclusiveBetween(1, 10).WithMessage("CalificacionCurso debe estar entre 1 y 10.");

        RuleFor(x => x.calificacionMaterial)
            .NotNull().WithMessage("CalificacionMaterial no puede ser nulo.")
            .InclusiveBetween(1, 10).WithMessage("CalificacionMaterial debe estar entre 1 y 10.");

        RuleFor(x => x.calificacionDocente)
            .NotNull().WithMessage("CalificacionDocente no puede ser nulo.")
            .InclusiveBetween(1, 10).WithMessage("CalificacionDocente debe estar entre 1 y 10.");

        RuleFor(x => x.comentarios)
            .DebeSerStringValido("Comentarios");
    }
}

