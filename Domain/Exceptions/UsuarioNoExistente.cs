namespace PlataformaDeGestionDeCursosOnline.Domain.Exceptions;

public class UsuarioNoExistente : Exception
{
    public UsuarioNoExistente(string mensaje) : base(mensaje){} 
    
}