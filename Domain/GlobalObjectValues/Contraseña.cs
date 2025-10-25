namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

public record Contraseña
{
    private string ValorContraseña;

    private Contraseña(string valorContraseña) => this.ValorContraseña = valorContraseña;
    
    
}