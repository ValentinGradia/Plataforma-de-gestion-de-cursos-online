using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Clases;

public class ReprogramarFechaDeClaseCommandValidator : AbstractValidator<ReprogramarFechaDeClaseCommand>
{
    public ReprogramarFechaDeClaseCommandValidator()
    {
        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");

        RuleFor(x => x.IdClase)
            .DebeSerGuidValido("IdClase");

        RuleFor(x => x.NuevaFecha)
            .DebeSerFechaValida("NuevaFecha")
            .GreaterThan(DateTime.UtcNow).WithMessage("NuevaFecha debe ser una fecha futura.");
    }
}

