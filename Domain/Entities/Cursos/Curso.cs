using System.Runtime.CompilerServices;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

public class Curso : Entity 
{
    public Profesor profesor;
    public EstadoCurso estadoCurso;
    public DateRange duracionCurso;
    public string nombreCurso;
    public string temarioCurso;
    //FIFO -> primer valor de la cola es la primera nota o examen que se agrego
    private Queue<Nota> notas;
    private Queue<Examen> examenes;
    
    public List<Estudiante> estudiantes;
    
    private Curso(Profesor profesor) : base(Guid.NewGuid())
    {
        this.profesor = profesor;
        this.estudiantes = new List<Estudiante>();
    }
    public static void CrearCurso(Profesor profesor)
    {
        //validaciones
        if (profesor == null)
            throw new ArgumentNullException(nameof(profesor));

        new Curso(profesor);
    }
    
    public void AgregarEstudiante(Estudiante estudiante)
    {
        if (estudiante == null)
            throw new ArgumentNullException(nameof(estudiante));
        
        this.estudiantes.Add(estudiante);
    }
    
    public void RemoverEstudiante(Estudiante estudiante)
    {
        if (estudiante == null)
            throw new ArgumentNullException(nameof(estudiante));
        
        this.estudiantes.Remove(estudiante);
    }
}