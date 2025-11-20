using System.Runtime.CompilerServices;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.ObjectValues;

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
    public List<Clase> clases;
    
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
    
    //CLASE
    
    public Clase IniciarClase(string material)
    {
        var clase = Clase.CrearClase(this.Id, material);
        this.clases.Add(clase);
        return clase;
    }
    public void FinalizarClase(Guid idClase)
    {
        Clase clase = this.clases.FirstOrDefault(c => c.Id == idClase);
        
        if (clase == null)
            throw new InvalidOperationException("Clase no encontrada en este curso");
        
        if (clase.Estado == EstadoClase.Completada)
            throw new InvalidOperationException("La clase ya fue finalizada");

        clase.Estado = EstadoClase.Completada;
    }
    

}