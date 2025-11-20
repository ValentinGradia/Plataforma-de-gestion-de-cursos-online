using System.Runtime.CompilerServices;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

public class Curso : Entity 
{
    public Profesor Profesor;
    public EstadoCurso EstadoCurso;
    public DateRange DuracionCurso;
    public string NombreCurso;
    public string TemarioCurso;
    //FIFO -> primer valor de la cola es la primera nota o examen que se agrego
    private List<Nota> notas;
    private Queue<Examen> examenes;
    public List<Clase> clases;
    
    private List<Inscripcion> InscripcionesEstudiantes;
    
    private Curso(Profesor profesor) : base(Guid.NewGuid())
    {
        this.Profesor = profesor;
        this.InscripcionesEstudiantes = new List<Inscripcion>();
    }
    public static void CrearCurso(Profesor profesor)
    {
        //validaciones
        if (profesor == null)
            throw new ArgumentNullException(nameof(profesor));

        new Curso(profesor);
    }
    
    //INSCRIPCION ESTUDIANTE
    //nostros agreamos Inscripciones de los estudiantes, no el estudiante directamente
    //la relacion entre estudiante y curso es la inscripcion
    public void AgregarEstudiante(Inscripcion inscripcionEstudiante)
    {
        if (inscripcionEstudiante ==  null)
            throw new ArgumentNullException(nameof(inscripcionEstudiante));
        
        this.InscripcionesEstudiantes.Add(inscripcionEstudiante);
    }
    
    public void RemoverEstudiante(Inscripcion inscripcionEstudiante)
    {
        if (inscripcionEstudiante == null)
            throw new ArgumentNullException(nameof(inscripcionEstudiante));
        
        this.InscripcionesEstudiantes.Remove(inscripcionEstudiante);
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