namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;

public class AsistenciaYaCargada : Exception
{
    public AsistenciaYaCargada() : base("Ya esta cargada la asistencia")
    {
        
    }
}