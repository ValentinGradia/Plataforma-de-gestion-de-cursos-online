using System.Runtime.CompilerServices;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;
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
    public Profesor Profesor { get; private set; }
    public EstadoCurso Estado { get; private set; }
    public DateRange Duracion { get; private set; }
    public string Nombre { get; private set; }
    public string Temario { get; private set; }
    private readonly List<Examen> _examenes;
    public readonly List<Clase> _clases;
    public readonly List<Encuesta> _encuestas = new List<Encuesta>();
    private int limiteDeEstudiantes = 30;
    public int cantidadDeInscriptos => this._inscripcionesEstudiantes.Count;
    
    private readonly List<Inscripcion> _inscripcionesEstudiantes = new();
    public IReadOnlyCollection<Inscripcion> Inscripciones => _inscripcionesEstudiantes.AsReadOnly();
    
    private Curso(Profesor profesor, string temario, string nombre, DateTime inicio, DateTime fin) : base(Guid.NewGuid())
    {
        this.Profesor = profesor;
        this.Temario = temario;
        this.Nombre = nombre;
        this.Estado = EstadoCurso.Disponible;
        this.Duracion = DateRange.CrearDateRange(inicio, fin);
        this._inscripcionesEstudiantes = new List<Inscripcion>();
        this._examenes = new List<Examen>();
        this._clases = new List<Clase>();
    }
    public static Curso CrearCurso(Profesor profesor, string temario, string nombreCurso, DateTime inicio, DateTime fin)
    {
        //validaciones
        if (profesor == null)
            throw new ArgumentNullException(nameof(profesor));

        return new Curso(profesor,temario,nombreCurso,inicio,fin);
    }

    public void IniciarCurso()
    {
        this.Estado = EstadoCurso.EnProgreso;
        this.RaiseDomainEvent(new CursoIniciado(this.Id, DateTime.Now));
    }
    
    public void FinalizarCurso()
    {
        this.Estado = EstadoCurso.Finalizado;
        this.RaiseDomainEvent(new CursoFinalizado(this.Id, DateTime.Now));
    }
    
    public void ActualizarNombre(string nuevoNombre)
    {
        if (string.IsNullOrWhiteSpace(nuevoNombre))
            throw new ArgumentException("Nombre inválido", nameof(nuevoNombre));

        this.Nombre = nuevoNombre.Trim();
    }
    
    public void ActualizarTemario(string nuevoTemario)
    {
        if (string.IsNullOrWhiteSpace(nuevoTemario))
            throw new ArgumentException("Temario inválido", nameof(nuevoTemario));

        this.Temario = nuevoTemario.Trim();
    }
    
    public IReadOnlyList<Clase> ObtenerClases()
    {
        return this._clases.AsReadOnly();
    }
    
    //INSCRIPCION -> ESTUDIANTE
    //nostros agregamos Inscripciones de los estudiantes, no el estudiante directamente
    //la relacion entre estudiante y curso es la inscripcion
    public void AgregarEstudiante(Inscripcion inscripcionEstudiante)
    {
        if (inscripcionEstudiante ==  null)
            throw new ArgumentNullException(nameof(inscripcionEstudiante));
        
        this.ValidarSiElCursoEstaDisponible();
        this.ValidarLimiteDeEstudiantes();
        this.ValidarSiElEstudianteYaPerteneceAlCurso(inscripcionEstudiante.IdEstudiante);
        
        this._inscripcionesEstudiantes.Add(inscripcionEstudiante);
        this.RaiseDomainEvent(new UsuarioInscriptoEnCurso(inscripcionEstudiante.IdEstudiante, DateTime.Now));
    }
    
    public void RemoverEstudiante(Inscripcion inscripcionEstudiante)
    {
        if (inscripcionEstudiante == null)
            throw new ArgumentNullException(nameof(inscripcionEstudiante));
        
        this._inscripcionesEstudiantes.Remove(inscripcionEstudiante);
    }
    
    public void DarseDeBajaEstudiante(Guid IdEstudiante)
    {
        Inscripcion inscripcionEstudiante =
            this._inscripcionesEstudiantes.FirstOrDefault(i => i.IdEstudiante == IdEstudiante);

        inscripcionEstudiante.DarseDeBaja();
    }
    
    //VALIDACIONES
    
    public void ValidarLimiteDeEstudiantes()
    {
        if (this._inscripcionesEstudiantes.Count == this.limiteDeEstudiantes)
            throw new LimiteDeEstudiantesAlcanzadoException();
    }
    
    public void ValidarSiElCursoEstaDisponible()
    {
        if (this.Estado != EstadoCurso.Disponible)
            throw new CursoNoDisponibleException();
    }
    
    public void ValidarSiElEstudianteYaPerteneceAlCurso(Guid idEstudiante)
    {
        if (this._inscripcionesEstudiantes.Any(i => i.IdEstudiante == idEstudiante))
            throw new EstudianteYaInscriptoException();
    }
    
    //CLASES
    public Guid IniciarClase(string material)
    {
        Clase clase = Clase.CrearClase(this, material);
        this._clases.Add(clase);
        clase.IniciarClase();
        return clase.Id;
    }
    public void FinalizarClase(Guid idClase)
    {
        Clase clase = this._clases.FirstOrDefault(c => c.Id == idClase)!;
        
        if (clase == null)
            throw new InvalidOperationException("Clase no encontrada en este curso");
        
        if (clase.Estado == EstadoClase.Finalizada)
            throw new InvalidOperationException("La clase ya fue finalizada");

        clase.FinalizarClase();
    }
    
    //EXAMENES
    public Guid CargarExamen(TipoExamen tipoExamen, string temaExamen, DateTime fechaLimiteDeEntrega)
    {
        
        if (fechaLimiteDeEntrega < this.Duracion.Inicio || fechaLimiteDeEntrega > this.Duracion.Fin)
            throw new ArgumentOutOfRangeException("La fecha límite debe estar dentro del período del curso");
        
        Examen examen =  Examen.CrearExamen(this.Id, tipoExamen, temaExamen, fechaLimiteDeEntrega);
        this._examenes.Add(examen);
        this.RaiseDomainEvent(new ExamenSubido(examen.Id, examen.FechaExamenCargado));
        return examen.Id;
    }
    
    
    //PROFESORES
    public void CambiarProfesor(Profesor nuevoProfesor)
    {
        if (nuevoProfesor == null)
            throw new ArgumentNullException(nameof(nuevoProfesor));

        this.Profesor = nuevoProfesor;
    }
    
    //ENCUESTAS
    //Recibimos los parametros para crear la encuesta (y no recibir
    // el objeto Encuesta) aca para poder
    //cambiar reglas o logica de negocio dentro del metodo del curso
    public void AgregarEncuesta(
        Guid? idEstudiante,
        int calificacionCurso,
        int calificacionMaterial,
        int calificacionDocente,
        string comentarios = null)
    {
        Encuesta encuesta = Encuesta.Crear(
            this.Id,
            idEstudiante,
            calificacionCurso,
            calificacionMaterial,
            calificacionDocente,
            comentarios);
        
        //un estudiante no puede repetir encuesta
        if (_encuestas.Any(e => e.IdEstudiante == idEstudiante))
        {
            throw new EstudianteYaCreoEncuestaException();
        }

        this._encuestas.Add(encuesta);
    }
    
}