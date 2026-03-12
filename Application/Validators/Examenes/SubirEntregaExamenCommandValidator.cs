using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Examenes;

public class SubirEntregaExamenCommandValidator : AbstractValidator<SubirEntregaExamenCommand>
{
    public SubirEntregaExamenCommandValidator()
    {
        RuleFor(x => x.IdExamen)
            .DebeSerGuidValido("IdExamen");

        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.IdEstudiante)
            .DebeSerGuidValido("IdEstudiante");

        RuleFor(x => x.Respuesta)
            .DebeSerStringValido("Respuesta");

        RuleFor(x => x.FechaLimite)
            .DebeSerFechaValida("FechaLimite");
    }
}

