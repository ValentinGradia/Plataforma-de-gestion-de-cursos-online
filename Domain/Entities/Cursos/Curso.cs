using System.Runtime.CompilerServices;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

public class Curso : Entity 
{
    private Guid idCurso;
    public Profesor profesor;
    public EstadoCurso estadoCurso;
    public DateRange duracionCurso;
    public string nombreCurso;
    public string temarioCurso;
    
    
    public List<Estudiante> estudiantes;
    
    private Curso(Guid id, Profesor profesor) : base(id)
    {
        this.profesor = profesor;
        this.estudiantes = new List<Estudiante>();
    }
    public static void CrearCurso(Guid id ,Profesor profesor)
    {
        //validaciones
        if (profesor == null)
            throw new ArgumentNullException(nameof(profesor));

        new Curso(id, profesor);
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