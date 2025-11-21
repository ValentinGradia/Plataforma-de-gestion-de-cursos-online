namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Exceptions;

public class CursoNoDisponibleException : Exception
{
    public CursoNoDisponibleException() : base("El curso no está disponible para inscripción.")
    {
    }
}