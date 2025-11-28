namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Exceptions;

public class EstudianteNoPerteneceAlCurso : Exception
{
    public EstudianteNoPerteneceAlCurso() : base("El estudiante no pertenece al curso especificado")
    {
        
    }
}