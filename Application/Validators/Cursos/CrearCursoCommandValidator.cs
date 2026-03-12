using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Cursos;

public class CrearCursoCommandValidator : AbstractValidator<CrearCursoCommand>
{
    public CrearCursoCommandValidator()
    {
        RuleFor(x => x.IdProfesor)
            .DebeSerGuidValido("IdProfesor");

        RuleFor(x => x.temario)
            .DebeSerStringValido("Temario");

        RuleFor(x => x.nombreCurso)
            .DebeSerStringValido("NombreCurso");

        RuleFor(x => x.inicio)
            .DebeSerFechaValida("Inicio");

        RuleFor(x => x.fin)
            .DebeSerFechaValida("Fin")
            .GreaterThan(x => x.inicio).WithMessage("La fecha de fin debe ser posterior a la fecha de inicio.");
    }
}

