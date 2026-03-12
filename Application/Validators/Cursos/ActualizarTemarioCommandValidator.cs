using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Cursos;

public class ActualizarTemarioCommandValidator : AbstractValidator<ActualizarTemarioCommand>
{
    public ActualizarTemarioCommandValidator()
    {
        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.Temario)
            .DebeSerStringValido("Temario");
    }
}

