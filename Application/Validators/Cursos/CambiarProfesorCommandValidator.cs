using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Cursos;

public class CambiarProfesorCommandValidator : AbstractValidator<CambiarProfesorCommand>
{
    public CambiarProfesorCommandValidator()
    {
        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.IdProfesor)
            .DebeSerGuidValido("IdProfesor");
    }
}

