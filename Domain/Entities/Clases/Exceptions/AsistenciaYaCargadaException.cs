namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases.Exceptions;

public class AsistenciaYaCargadaException : Exception
{
    public AsistenciaYaCargadaException() : base("Ya esta cargada la asistencia")
    {
        
    }
}