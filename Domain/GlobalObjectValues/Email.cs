namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

public record Email
{
    private string valorEmail;

    private Email(string valorEmail) => this.valorEmail = valorEmail;

    private static Email CrearEmail(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentNullException("El email no puede estar vacío.", nameof(valor));

        if (!valor.Contains("@"))
            throw new ArgumentException("El email debe contener el carácter '@'.", nameof(valor));

        return new Email(valor);
    }
};