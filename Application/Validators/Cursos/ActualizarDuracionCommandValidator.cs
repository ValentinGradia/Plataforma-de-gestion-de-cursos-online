using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Cursos;

public class ActualizarDuracionCommandValidator : AbstractValidator<ActualizarDuracionCommand>
{
    public ActualizarDuracionCommandValidator()
    {
        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.NuevaDuracion)
            .NotNull().WithMessage("NuevaDuracion no puede ser nula.");
    }
}

