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
    private Guid IdCurso { get; }
    public string Material { get; private set; }
    //Quien maneja el estado de las clases es el curso
    public EstadoClase Estado;
    public DateTime Fecha { get; private set; }
    private readonly List<Asistencia> _asistencias = new List<Asistencia>();
    public IReadOnlyCollection<Asistencia> Asistencias => this._asistencias;
    
    public  List<Consulta> _consultas { get; private set; } 

    private Clase(Guid Idcurso, string material, EstadoClase estado) : base(Guid.NewGuid())
    {
        this.IdCurso = Idcurso;
        this.Material = material;
        this.Fecha = DateTime.UtcNow;
        this.Estado = estado;
    }

    public static Clase CrearClase(Guid IdCurso, string material)
    {
        return new Clase(IdCurso, material, EstadoClase.EnCurso);
    }
    
    public ICollection<Asistencia> ObtenerAsistencias()
    {
        return _asistencias.AsReadOnly();
    }
    
    public void IniciarClase()
    {
        this.Estado = EstadoClase.EnCurso;
        this.RaiseDomainEvent(new ClaseIniciada(this.Id, DateTime.Now));
    }
    
    public void FinalizarClase()
    {
        this.Estado = EstadoClase.Finalizada;
        this.RaiseDomainEvent(new ClaseFinalizada(this.Id, DateTime.Now));
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
    
    public void AgregarConsulta(string titulo, string descripcion,  Guid IdUsuario)
    {
        if (this._consultas == null)
            this._consultas = new List<Consulta>();
        
        Consulta consulta = new Consulta(IdUsuario,titulo, descripcion, DateTime.UtcNow);
        this._consultas.Add(consulta);
        this.RaiseDomainEvent(new ConsultaCargada(this.Id,IdUsuario ,consulta.FechaConsulta));
    }

}