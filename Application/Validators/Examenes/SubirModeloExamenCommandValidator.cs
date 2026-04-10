using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos.Examenes;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Examenes;

public class SubirModeloExamenCommandValidator : AbstractValidator<SubirModeloExamenCommand>
{
    public SubirModeloExamenCommandValidator()
    {
        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.TemaExamen)
            .DebeSerStringValido("TemaExamen");

        RuleFor(x => x.FechaLimite)
            .DebeSerFechaValida("FechaLimite")
            .GreaterThan(DateTime.UtcNow).WithMessage("FechaLimite debe ser una fecha futura.");
    }
}

