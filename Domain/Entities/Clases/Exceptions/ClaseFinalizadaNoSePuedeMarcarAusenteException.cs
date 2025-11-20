namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;

public class ClaseFinalizadaNoSePuedeMarcarAusenteException : Exception
{
    public ClaseFinalizadaNoSePuedeMarcarAusenteException() : base("No se puede dar presente una vez finalizada la clase")
    {
        
    }
}