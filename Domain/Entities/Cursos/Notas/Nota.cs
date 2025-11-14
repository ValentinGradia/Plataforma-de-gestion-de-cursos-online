using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

public class Nota<T,U> where T : Curso  where U : Estudiante
{
    private T curso;
    private U Estudiante;
    //usamos una cola para que este ordenado en base que examen rindio primero -> FIFO 
    //el primer valor sera la nota del primer examen rendido
    private Queue<int> notas = new();
    
    public void AsignarNota(int nota)
    {
        this.notas.Enqueue(nota);
    }
    
    public double ObtenerNotaPromedio()
    {
        if (notas.Count == 0)
            throw new InvalidOperationException("No hay calificaciones asignadas.");
        
        return notas.Average();
    }
}