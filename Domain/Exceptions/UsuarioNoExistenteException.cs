namespace PlataformaDeGestionDeCursosOnline.Domain.Exceptions;

public class UsuarioNoExistenteException : Exception
{
    public UsuarioNoExistenteException(string mensaje) : base(mensaje){} 
    
}