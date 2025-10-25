namespace PlataformaDeGestionDeCursosOnline.Domain.Exceptions;

public class ContraseñaIncorrecta : Exception
{
    public ContraseñaIncorrecta(string mensaje) : base(mensaje){}
}