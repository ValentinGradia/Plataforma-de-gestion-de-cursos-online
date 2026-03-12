using FluentValidation;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Usuarios;
using PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Usuarios;

public class ActualizarContactoCommandValidator : AbstractValidator<ActualizarContactoCommand>
{
    public ActualizarContactoCommandValidator()
    {
        RuleFor(x => x.IdUsuario)
            .DebeSerGuidValido("IdUsuario");

        RuleFor(x => x.Email)
            .DebeSerStringValido("Email")
            .EmailAddress().WithMessage("Email no tiene un formato válido.");
    }
}

