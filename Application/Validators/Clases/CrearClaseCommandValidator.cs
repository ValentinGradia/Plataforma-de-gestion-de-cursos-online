using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos.Clases;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Clases;

public class CrearClaseCommandValidator : AbstractValidator<CrearClaseCommand>
{
    public CrearClaseCommandValidator()
    {
        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.Material)
            .DebeSerStringValido("Material");
    }
}

