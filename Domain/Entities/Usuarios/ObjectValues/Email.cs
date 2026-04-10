namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios.ObjectValues;


public record Email
{
    public string valorEmail { get; private set; }

    private Email(string valorEmail) => this.valorEmail = valorEmail;

    public static Email CrearEmail(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentNullException("El email no puede estar vacío.", nameof(valor));

        if (!valor.Contains("@"))
            throw new ArgumentException("El email debe contener el carácter '@'.", nameof(valor));

        return new Email(valor);
    }
};