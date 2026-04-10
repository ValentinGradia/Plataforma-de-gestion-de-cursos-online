namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios.ObjectValues;

public record Contraseña
{
    public string ValorContraseña { get; private set; }

    private Contraseña(string valorContraseña) => this.ValorContraseña = valorContraseña;

    public static Contraseña CrearContraseña(string valor)
    {
        if (string.IsNullOrEmpty(valor))
            throw new ArgumentNullException(nameof(valor));

        return new Contraseña(valor);
    }
    
    
}