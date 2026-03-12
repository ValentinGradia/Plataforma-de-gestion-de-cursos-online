using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Examenes;

public class PonerNotaEntregaExamenCommandValidator : AbstractValidator<PonerNotaEntregaExamenCommand>
{
    public PonerNotaEntregaExamenCommandValidator()
    {
        RuleFor(x => x.IdEntregaExamen)
            .DebeSerGuidValido("IdEntregaExamen");

        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.IdProfesor)
            .DebeSerGuidValido("IdProfesor");

        RuleFor(x => x.NuevaNota)
            .DebeSerDecimalValido("NuevaNota")
            .LessThanOrEqualTo(10).WithMessage("NuevaNota no puede ser mayor a 10.");
    }
}

