namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Exceptions;

public class LimiteDeEstudiantesAlcanzadoException : Exception
{
    public LimiteDeEstudiantesAlcanzadoException() : base("Se ha alcanzado el limite de estudiantes para este curso.")
    {
    }
}