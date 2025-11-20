namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;

public class CursoLlenoException : Exception
{
    public CursoLlenoException() : base("No se permiten mas estudiantes en el curso")
    {
        
    }
}