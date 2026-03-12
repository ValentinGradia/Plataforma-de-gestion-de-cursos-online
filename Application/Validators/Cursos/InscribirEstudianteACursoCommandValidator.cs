using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Cursos;

public class InscribirEstudianteACursoCommandValidator : AbstractValidator<InscribirEstudianteACursoCommand>
{
    public InscribirEstudianteACursoCommandValidator()
    {
        RuleFor(x => x.IdEstudiante)
            .DebeSerGuidValido("IdEstudiante");

        RuleFor(x => x.IdCurso)
            .DebeSerGuidValido("IdCurso");
    }
}

