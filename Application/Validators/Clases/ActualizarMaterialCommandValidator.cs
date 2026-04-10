using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos.Clases;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Clases;

public class ActualizarMaterialCommandValidator : AbstractValidator<ActualizarMaterialCommand>
{
    public ActualizarMaterialCommandValidator()
    {
        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.IdClase)
            .DebeSerGuidValido("IdClase");

        RuleFor(x => x.NuevoMaterial)
            .DebeSerStringValido("NuevoMaterial");
    }
}

