using System.Runtime.CompilerServices;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Interfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

public class Curso : Entity, ICicloDeVidaDelCurso
{
    public Guid IdProfesor { get; private set; }
    public EstadoCurso Estado { get; private set; }
    public DateRange Duracion { get; private set; }
    public string Nombre { get; private set; }
    public string Temario { get; private set; }
    public readonly List<Examen> _examenes;
    public readonly List<Clase> _clases;
    private int limiteDeEstudiantes = 30;
    public int cantidadDeInscriptos => this._inscripcionesEstudiantes.Count;

    private readonly List<Inscripcion> _inscripcionesEstudiantes = new();
    public IReadOnlyCollection<Inscripcion> Inscripciones => _inscripcionesEstudiantes.AsReadOnly();

    // Nuevo: obtener la inscripción correspondiente a un estudiante por su Id
    public Inscripcion? ObtenerInscripcionPorEstudiante(Guid idEstudiante)
    {
        return this._inscripcionesEstudiantes.FirstOrDefault(i => i.IdEstudiante == idEstudiante);
    }
    
    private Curso() : base() { }

    private Curso(Guid profesorId, string temario, string nombre, DateTime inicio, DateTime fin) : base(
        Guid.NewGuid())
    {
        this.IdProfesor = profesorId;
        this.Temario = temario;
        this.Nombre = nombre;
        this.Estado = EstadoCurso.Disponible;
        this.Duracion = DateRange.CrearDateRange(inicio, fin);
        this._inscripcionesEstudiantes = new List<Inscripcion>();
        this._examenes = new List<Examen>();
        this._clases = new List<Clase>();
    }
    
    private Curso(Guid Id, Guid profesorId, string temario, string nombre, EstadoCurso estado ,DateRange duracion, List<Inscripcion> inscripciones,
        List<Examen> examenes, List<Clase> clases): base(Id)
    {
        this.IdProfesor = profesorId;
        this.Temario = temario;
        this.Nombre = nombre;
        this.Estado = estado;
        this.Duracion = duracion;
        this.SetearInscripciones(inscripciones);
        this.SetearExamenes(examenes);
        this.SetearClases(clases);
    }
    
    public static Curso ReconstruirCurso(Guid id, Guid idProfesor, string temario, string nombre, EstadoCurso estado ,DateRange duracion, List<Inscripcion> inscripciones,
        List<Examen> examenes, List<Clase> clases)
    {
        return new Curso(id, idProfesor, temario, nombre, estado ,duracion, inscripciones, examenes, clases);
    }

    public static Curso CrearCurso(Guid profesorId, string temario, string nombreCurso, DateTime inicio, DateTime fin)
    {
        //validaciones
        if (profesorId == null)
            throw new ArgumentNullException("El profesorId no puede ser nulo");

        return new Curso(profesorId,temario, nombreCurso, inicio, fin);
    }
    
    // Setear entidades
    private void SetearInscripciones(List<Inscripcion> inscripciones)
    {
        this._inscripcionesEstudiantes.Clear();
        this._inscripcionesEstudiantes.AddRange(inscripciones);
    }
    
    private void SetearExamenes(List<Examen> examenes)
    {
        this._examenes.Clear();
        this._examenes.AddRange(examenes);
    }
    
    private void SetearClases(List<Clase> clases)
    {
        this._clases.Clear();
        this._clases.AddRange(clases);
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
        this.ValidarSiElEstudianteYaPerteneceAlCurso(inscripcionEstudiante.Id);
        
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
        
        if(inscripcion is null)
            throw new ArgumentOutOfRangeException("No se encontró la inscripción con el ID proporcionado.");
        
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
    
    public void ValidarSiElEstudianteYaPerteneceAlCurso(Guid idInscripcionEstudiante)
    {
        if (this._inscripcionesEstudiantes.Any(i => i.Id == idInscripcionEstudiante))
            throw new EstudianteYaInscriptoException();
    }
    
    public void ValidarSiElEstudianteNoPerteneceAlCurso(Guid idInscripcionEstudiante)
    {
        if (!(this._inscripcionesEstudiantes.Any(i => i.Id == idInscripcionEstudiante)))
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
        if (this.IdProfesor == idProfesor)
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

        if (examen is null)
        {
            throw new ArgumentOutOfRangeException("Examen no encontrado en este curso.");
        }
        return examen;
    }
    
    //PROFESORES
    public void CambiarProfesor(Guid nuevoProfesor)
    {
        
        this.IdProfesor = nuevoProfesor;
    }
    

}