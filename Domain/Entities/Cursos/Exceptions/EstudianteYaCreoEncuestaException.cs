namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Exceptions;

public class EstudianteYaCreoEncuestaException : Exception
{
    public EstudianteYaCreoEncuestaException() : base("El estudiante ya ha creado una encuesta para este curso.")
    {
    }
}