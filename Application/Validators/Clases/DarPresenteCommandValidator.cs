using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Clases;

public class DarPresenteCommandValidator : AbstractValidator<DarPresenteCommand>
{
    public DarPresenteCommandValidator()
    {
        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.IdClase)
            .DebeSerGuidValido("IdClase");

        RuleFor(x => x.IdInscripcionEstudiante)
            .DebeSerGuidValido("IdInscripcionEstudiante");
    }
}

