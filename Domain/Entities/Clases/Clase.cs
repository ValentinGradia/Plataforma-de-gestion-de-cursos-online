using System.Runtime.InteropServices.JavaScript;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities;

public class Clase : Entity
{
    public string Material;
    public EstadoClase Estado;
    public Guid IdCurso { get; private set; }
    public DateTime Fecha { get; }
    private readonly List<Asistencia> _asistencias;
    
    //se van a mostrar en orden de cual fue la ultima 
    public Queue<Consulta> consultasDeAlumnos;

    private Clase(Guid IdCurso, string material, EstadoClase estado) : base(Guid.NewGuid())
    {
        this.IdCurso = IdCurso;
        this.Material = material;
        this.Fecha = DateTime.UtcNow;
        this.Estado = estado;
    }

    public static Clase IniciarClase(Guid idCurso, string material)
    {
        return new Clase(idCurso, material, EstadoClase.EnCurso);
    }

    public void FinalizarClase()
    {
        if (Estado == EstadoClase.Completada)
            throw new InvalidOperationException("La clase ya fue finalizada");

        Estado = EstadoClase.Completada;
    }
    
    public void DarPresente(Guid IdEstudiante)
    {
        if (_asistencias.Any(a => a.IdEstudiante == IdEstudiante))
            throw new AsistenciaYaCargada();

        Asistencia asistencia = new Asistencia(IdEstudiante, true);
        _asistencias.Add(asistencia);
    }
    
    public void DarAusente(Guid IdEstudiante)
    {
        if (_asistencias.Any(a => a.IdEstudiante == IdEstudiante))
            throw new AsistenciaYaCargada();

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