namespace PlataformaDeGestionDeCursosOnline.Domain.Exceptions;

public class ContraseñaIncorrectaException : Exception
{
    public ContraseñaIncorrectaException(string mensaje) : base(mensaje){}
}