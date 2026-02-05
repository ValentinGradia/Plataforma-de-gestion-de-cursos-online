using System.Runtime.InteropServices.JavaScript;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Events;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.ObjectValues;

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
    
    //ACTUALIZAR ESTO EN LAS ASISTENCIAS DE LAS INSCRIPCIONES DE LOS ESTUDIANTES
    //(El dar presente y ausante)
    public void DarPresente(Guid IdEstudiante)
    {
        if (_asistencias.Any(a => a.IdEstudiante == IdEstudiante))
            throw new AsistenciaYaCargadaException();
        
        if(Estado == EstadoClase.Finalizada)
            throw new ClaseFinalizadaNoSePuedeMarcarPresenteException();

        Asistencia asistencia = new Asistencia(IdEstudiante, true);
        _asistencias.Add(asistencia);
    }
    
    public void DarAusente(Guid IdEstudiante)
    {
        if (_asistencias.Any(a => a.IdEstudiante == IdEstudiante))
            throw new AsistenciaYaCargadaException();

        Asistencia asistencia = new Asistencia(IdEstudiante, false);
        _asistencias.Add(asistencia);
    }
    
    public Consulta AgregarConsulta(string titulo, string descripcion,  Guid IdEstudiante)
    {
        if (this._consultas == null)
            this._consultas = new List<Consulta>();
        
        Consulta consulta = new Consulta(IdEstudiante,titulo, descripcion, DateTime.UtcNow);
        this._consultas.Add(consulta);
        this.RaiseDomainEvent(new ConsultaCargada(this.Id, IdEstudiante ,consulta.FechaConsulta));

        return consulta;
    }

}