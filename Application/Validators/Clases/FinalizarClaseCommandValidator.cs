using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos.Clases;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Clases;

public class FinalizarClaseCommandValidator : AbstractValidator<FinalizarClaseCommand>
{
    public FinalizarClaseCommandValidator()
    {
        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.IdClase)
            .DebeSerGuidValido("IdClase");
    }
}

