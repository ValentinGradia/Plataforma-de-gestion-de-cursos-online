using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

public class CalificacionEstudianteService<T> where T : Curso
{
    private T curso;
    private Guid IdEstudiante;
    private Queue<int> calificaciones = new();
    
    public void AsignarCalificacion(Guid estudiante, int calificacion)
    {
        this.IdEstudiante = estudiante;
        this.calificaciones.Enqueue(calificacion);
    }
    
    public double ObtenerCalificacionPromedio(Guid IdEstudiante)
    {
        if (calificaciones.Count == 0)
            throw new InvalidOperationException("No hay calificaciones asignadas.");
        
        return calificaciones.Average();
    }
}