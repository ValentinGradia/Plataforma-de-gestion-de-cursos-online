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
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

public class Curso : Entity, ICicloDeVidaDelCurso
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

    // Nuevo: obtener la inscripción correspondiente a un estudiante por su Id
    public Inscripcion? ObtenerInscripcionPorEstudiante(Guid idEstudiante)
    {
        return this._inscripcionesEstudiantes.FirstOrDefault(i => i.IdEstudiante == idEstudiante);
    }

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
    }
    
    public void FinalizarCurso()
    {
        this.Estado = EstadoCurso.Finalizado;
    }
    
    
    public void ActualizarTemario(string nuevoTemario)
    {
        if (string.IsNullOrWhiteSpace(nuevoTemario))
            throw new ArgumentException("Temario inválido", nameof(nuevoTemario));

        this.Temario = nuevoTemario.Trim();
    }

    // Nuevo: permitir actualizar la duración del curso
    public void ActualizarDuracion(ObjectValues.DateRange nuevaDuracion)
    {
        if (nuevaDuracion is null)
            throw new ArgumentNullException(nameof(nuevaDuracion));

        // Reutilizamos la validación de DateRange al crear uno nuevo a partir de las fechas
        this.Duracion = ObjectValues.DateRange.CrearDateRange(nuevaDuracion.Inicio, nuevaDuracion.Fin);
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

        this.RemoverEstudiante(inscripcionEstudiante);
        inscripcionEstudiante.DarseDeBaja();
    }
    
    public Inscripcion ObtenerInscripcionPorId(Guid idInscripcion)
    {
        Inscripcion inscripcion = this._inscripcionesEstudiantes.FirstOrDefault(i => i.Id == idInscripcion)!;
        
        return inscripcion;
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
    
    public void ValidarSiElEstudianteNoPerteneceAlCurso(Guid idEstudiante)
    {
        if (!(this._inscripcionesEstudiantes.Any(i => i.IdEstudiante == idEstudiante)))
            throw new EstudianteNoPerteneceAlCurso();
    }
    
    //CLASES
    public Guid IniciarClase(string material)
    {
        Clase clase = Clase.CrearClase(this.Id, material);
        this.AgregarClase(clase);
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
    
    public Clase ObtenerClase(Guid idClase)
    {
        Clase clase = this._clases.FirstOrDefault(c => c.Id == idClase)!;
        
        return clase;
    }
    
    public void AgregarClase(Clase clase)
    {
        if (clase == null)
            throw new ArgumentNullException(nameof(clase));

        this._clases.Add(clase);
    }
    
    //EXAMENES
    public Guid CargarExamen(Examen examen)
    {
        this._examenes.Add(examen);
        this.RaiseDomainEvent(new ExamenSubido(this.Id,examen.Id, examen.FechaExamenCargado));
        return examen.Id;
    }
    
    //Metodo por el cual asignamos la nota de una entrega a traves del curso
    public void CargarCalificacionAEntregaDeExamen(Guid idEntregaDelExamen, Guid idProfesor, decimal nuevaNota)
    {
        if (this.Profesor.Id == idProfesor)
        {
            EntregaDelExamen entrega = this.ObtenerEntregaDeExamen(idEntregaDelExamen);
            entrega.AsignarNota(nuevaNota);
        }
        else
        {
            throw new UnauthorizedAccessException("Solo el profesor del curso puede asignar notas a las entregas de los examenes");
        }
        
    }
    
    public EntregaDelExamen ObtenerEntregaDeExamen(Guid idEntregaDelExamen)
    {
        foreach (Inscripcion inscripcion in _inscripcionesEstudiantes)
        {
            EntregaDelExamen entrega = inscripcion.ObtenerEntregaDeExamen(idEntregaDelExamen);
            if (entrega != null)
                return entrega;
        }

        throw new ArgumentOutOfRangeException("Entrega no encontrada.");

    }
    
    public Examen ObtenerModeloExamen(Guid idExamen)
    {
        Examen examen = this._examenes.FirstOrDefault(e => e.Id == idExamen)!;

        return examen;
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