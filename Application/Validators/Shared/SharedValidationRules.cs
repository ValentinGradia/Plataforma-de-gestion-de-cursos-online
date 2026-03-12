using FluentValidation;

namespace PlataformaDeGestionDeCursosOnline.Application.Validators.Shared;

public static class SharedValidationRules
{
    public static IRuleBuilderOptions<T, Guid> DebeSerGuidValido<T>(
        this IRuleBuilder<T, Guid> ruleBuilder, string nombreCampo)
    {
        return ruleBuilder
            .NotEmpty().WithMessage($"{nombreCampo} no puede ser un Guid vacío.");
    }

    public static IRuleBuilderOptions<T, string> DebeSerStringValido<T>(
        this IRuleBuilder<T, string> ruleBuilder, string nombreCampo)
    {
        return ruleBuilder
            .NotNull().WithMessage($"{nombreCampo} no puede ser nulo.")
            .NotEmpty().WithMessage($"{nombreCampo} no puede ser un string vacío.");
    }

    public static IRuleBuilderOptions<T, DateTime> DebeSerFechaValida<T>(
        this IRuleBuilder<T, DateTime> ruleBuilder, string nombreCampo)
    {
        return ruleBuilder
            .NotEmpty().WithMessage($"{nombreCampo} no puede ser una fecha vacía.")
            .GreaterThan(DateTime.MinValue).WithMessage($"{nombreCampo} no es una fecha válida.");
    }

    public static IRuleBuilderOptions<T, decimal> DebeSerDecimalValido<T>(
        this IRuleBuilder<T, decimal> ruleBuilder, string nombreCampo)
    {
        return ruleBuilder
            .NotNull().WithMessage($"{nombreCampo} no puede ser nulo.")
            .GreaterThanOrEqualTo(0).WithMessage($"{nombreCampo} no puede ser negativo.");
    }
}

