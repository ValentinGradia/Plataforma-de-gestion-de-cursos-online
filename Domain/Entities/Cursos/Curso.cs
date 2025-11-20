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
    public EstadoCurso Estado;
    public DateRange Duracion;
    public string Nombre;
    public string Temario;
    private readonly List<Examen> _examenes;
    public readonly List<Clase> _clases;
    
    private readonly List<Inscripcion> _inscripcionesEstudiantes;
    
    private Curso(Profesor profesor, string temario, string nombre) : base(Guid.NewGuid())
    {
        this.Profesor = profesor;
        this.Temario = temario;
        this.Nombre = nombre;
        this._inscripcionesEstudiantes = new List<Inscripcion>();
    }
    public static void CrearCurso(Profesor profesor, string temario, string nombreCurso)
    {
        //validaciones
        if (profesor == null)
            throw new ArgumentNullException(nameof(profesor));

        new Curso(profesor,temario,nombreCurso);
    }
    
    //INSCRIPCION ESTUDIANTE
    //nostros agregamos Inscripciones de los estudiantes, no el estudiante directamente
    //la relacion entre estudiante y curso es la inscripcion
    public void AgregarEstudiante(Inscripcion inscripcionEstudiante)
    {
        if (inscripcionEstudiante ==  null)
            throw new ArgumentNullException(nameof(inscripcionEstudiante));
        
        this._inscripcionesEstudiantes.Add(inscripcionEstudiante);
    }
    
    public void RemoverEstudiante(Inscripcion inscripcionEstudiante)
    {
        if (inscripcionEstudiante == null)
            throw new ArgumentNullException(nameof(inscripcionEstudiante));
        
        this._inscripcionesEstudiantes.Remove(inscripcionEstudiante);
    }
    
    //CLASE
    public Clase IniciarClase(string material)
    {
        var clase = Clase.CrearClase(this, material);
        this._clases.Add(clase);
        return clase;
    }
    public void FinalizarClase(Guid idClase)
    {
        Clase clase = this._clases.FirstOrDefault(c => c.Id == idClase)!;
        
        if (clase == null)
            throw new InvalidOperationException("Clase no encontrada en este curso");
        
        if (clase.Estado == EstadoClase.Completada)
            throw new InvalidOperationException("La clase ya fue finalizada");
        
        int totalClases = this._clases.Count;

        clase.Estado = EstadoClase.Completada;
    }
    
    //EXAMEN
    public Examen CargarExamen(TipoExamen tipoExamen, string temaExamen, DateTime fechaLimiteDeEntrega)
    {
        return Examen.CrearExamen(this.Id, tipoExamen, temaExamen, fechaLimiteDeEntrega);
    }
    

}