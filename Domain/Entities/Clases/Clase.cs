using System.Runtime.InteropServices.JavaScript;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Events;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities;

//Los metodos de iniciar clase, finalizar y demas los va a menajar el curso porque
//es el curso quien asigna las clases y cambia los estados. La clase no puede existir sin
//el curso
public class Clase : Entity
{
    public Guid IdCurso { get; private set; }
    public string Material { get; private set; }
    //Quien maneja el estado de las clases es el curso
    public EstadoClase Estado;
    public DateTime Fecha { get; private set; }
    //Lo manejamos dentro de clase y no de curso porque estariamos duplicando informacion. Ademas,
    //quien maneja las asistencias es la clase.
    private readonly List<Asistencia> _asistencias = new List<Asistencia>();
    public IReadOnlyCollection<Asistencia> Asistencias => this._asistencias;
    
    public  List<Consulta> _consultas { get; private set; } 

    private Clase(Guid Idcurso, string material) : base(Guid.NewGuid())
    {
        this.IdCurso = Idcurso;
        this.Material = material;
        this.Fecha = DateTime.UtcNow;
        this.Estado = EstadoClase.EnCurso;
    }

    // Constructor interno para reconstrucción desde BD
    private Clase(Guid id, Guid idCurso, string material, DateTime fecha, EstadoClase estado, 
        List<Asistencia> asistencias, List<Consulta> consultas) : base(id)
    {
        this.IdCurso = idCurso;
        this.Material = material;
        this.Fecha = fecha;
        this.Estado = estado;
        this._asistencias = asistencias;
        this._consultas = consultas;
    }
    
    public static Clase ReconstruirClase(Guid id, Guid idCurso, string material, DateTime fecha, EstadoClase estado,
        List<Asistencia> asistencias, List<Consulta> consultas)
    {
        return new Clase(id, idCurso, material, fecha, estado, asistencias, consultas);
    }


    public static Clase CrearClase(Guid IdCurso, string material)
    {
        return new Clase(IdCurso, material);
    }

    public ICollection<Asistencia> ObtenerAsistencias()
    {
        return _asistencias.AsReadOnly();
    }
    
    public void IniciarClase()
    {
        this.Estado = EstadoClase.EnCurso;
    }
    
    public void FinalizarClase()
    {
        this.Estado = EstadoClase.Finalizada;
    }
    
    public void ActualizarMaterial(string nuevoMaterial)
    {
        this.Material = nuevoMaterial;
    }
    
    public void ReprogramarClase(DateTime nuevaFecha)
    {
        this.Fecha = nuevaFecha;
    }
    
    //Recibimos el id de inscripcion, no el id del estudiante porque en la inscripcion
    //guardamos el historial de asistencias
    public Asistencia DarPresente(Guid IdInscripcionDeEstudiante)
    {
        if (_asistencias.Any(a => a.IdInscripcionEstudiante == IdInscripcionDeEstudiante))
            throw new AsistenciaYaCargadaException();
        
        if(Estado == EstadoClase.Finalizada)
            throw new ClaseFinalizadaNoSePuedeMarcarPresenteException();

        Asistencia asistencia = new Asistencia(this.Id,IdInscripcionDeEstudiante,true);
        _asistencias.Add(asistencia);
        return asistencia;
    }
    
    public Asistencia DarAusente(Guid IdInscripcionEstudiante)
    {
        if (_asistencias.Any(a => a.IdInscripcionEstudiante == IdInscripcionEstudiante))
            throw new AsistenciaYaCargadaException();

        Asistencia asistencia = new Asistencia(this.Id,IdInscripcionEstudiante,false);
        _asistencias.Add(asistencia);
        return asistencia;
    }
    
    public void AgregarConsulta(string titulo, string descripcion,  Guid IdEstudiante)
    {
        if (this._consultas == null)
            this._consultas = new List<Consulta>();
        
        Consulta consulta = new Consulta(this.Id,IdEstudiante,titulo, descripcion, DateTime.UtcNow);
        this._consultas.Add(consulta);
        this.RaiseDomainEvent(new ConsultaCargada(this.IdCurso, IdEstudiante ,consulta.FechaConsulta));
        
    }

}