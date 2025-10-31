namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

public record Contraseña
{
    private string ValorContraseña;

    private Contraseña(string valorContraseña) => this.ValorContraseña = valorContraseña;

    public static Contraseña CrearContraseña(string valor)
    {
        if (string.IsNullOrEmpty(valor))
            throw new ArgumentNullException(nameof(valor));

        return new Contraseña(valor);
    }
}