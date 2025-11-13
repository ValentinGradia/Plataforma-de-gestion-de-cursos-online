using System.Runtime.CompilerServices;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

public class Curso : Entity
{
    private Guid idCurso;
    private CalificacionEstudianteService<Curso> calificacionEstudiante;
    public Profesor profesor;
    
    public List<Estudiante> estudiantes;
    
    private Curso(Guid id, Profesor profesor) : base(id)
    {
        this.profesor = profesor;
        this.calificacionEstudiante = new CalificacionEstudianteService<Curso>();
    }
    public static void CrearCurso(Guid id ,Profesor profesor)
    {
        //validaciones
        if (profesor == null)
            throw new ArgumentNullException(nameof(profesor));

        new Curso(id, profesor);
    }
    
    private void AsignarNotaEstudiante(Guid idEstudiante, int nota)
    {
        this.calificacionEstudiante.AsignarCalificacion(idEstudiante, nota);
    }
}