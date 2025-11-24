namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;

public class ClaseFinalizadaNoSePuedeMarcarPresenteException : Exception
{
    public ClaseFinalizadaNoSePuedeMarcarPresenteException() : base("No se puede dar presente una vez finalizada la clase")
    {
        
    }
}