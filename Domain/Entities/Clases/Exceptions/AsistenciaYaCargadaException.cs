namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;

public class AsistenciaYaCargadaException : Exception
{
    public AsistenciaYaCargadaException() : base("Ya esta cargada la asistencia")
    {
        
    }
}