namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Exceptions;

public class EstudianteYaInscriptoException : Exception
{
    public EstudianteYaInscriptoException() : base("El estudiante ya se encuentra inscripto en este curso.")
    {
    } 
}