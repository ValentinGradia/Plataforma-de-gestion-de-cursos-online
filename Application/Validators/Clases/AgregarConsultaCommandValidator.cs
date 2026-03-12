using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Clases;

public class AgregarConsultaCommandValidator : AbstractValidator<AgregarConsultaCommand>
{
    public AgregarConsultaCommandValidator()
    {
        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.IdEstudiante)
            .DebeSerGuidValido("IdEstudiante");

        RuleFor(x => x.IdClase)
            .DebeSerGuidValido("IdClase");

        RuleFor(x => x.Titulo)
            .DebeSerStringValido("Titulo");

        RuleFor(x => x.Descripcion)
            .DebeSerStringValido("Descripcion");
    }
}

