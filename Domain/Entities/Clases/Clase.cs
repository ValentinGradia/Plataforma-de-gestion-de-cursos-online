using System.Runtime.InteropServices.JavaScript;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities;

//Los metodos de iniciar clase, finalizar y demas los va a menajar el curso porque
//es el curso quien asigna las clases y cambia los estados. La clase no puede existir sin
//el curso
public class Clase : Entity
{
    public string Material;
    public EstadoClase Estado;
    public Curso curso { get; private set; }
    public DateTime Fecha { get; }
    private readonly List<Asistencia> _asistencias = new List<Asistencia>();
    public IReadOnlyCollection<Asistencia> Asistencias => this._asistencias;
    
    //se van a mostrar en orden de cual fue la ultima 
    public Queue<Consulta> consultasDeAlumnos;

    private Clase(Curso curso, string material, EstadoClase estado) : base(Guid.NewGuid())
    {
        this.curso = curso;
        this.Material = material;
        this.Fecha = DateTime.UtcNow;
        this.Estado = estado;
    }

    public static Clase CrearClase(Curso curso, string material)
    {
        return new Clase(curso, material, EstadoClase.EnCurso);
    }
    
    public ICollection<Asistencia> ObtenerAsistencias()
    {
        return _asistencias.AsReadOnly();
    }
    
    
    public void DarPresente(Guid IdEstudiante)
    {
        if (_asistencias.Any(a => a.IdEstudiante == IdEstudiante))
            throw new AsistenciaYaCargadaException();
        
        if(Estado == EstadoClase.Completada)
            throw new ClaseFinalizadaNoSePuedeMarcarAusenteException();

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
    
    public void AgregarConsulta(string titulo, string descripcion,  Guid IdEstudiante)
    {
        if (consultasDeAlumnos == null)
            consultasDeAlumnos = new Queue<Consulta>();
        
        var consulta = new Consulta(IdEstudiante, titulo, descripcion, DateTime.UtcNow);
        consultasDeAlumnos.Enqueue(consulta);
    }

}