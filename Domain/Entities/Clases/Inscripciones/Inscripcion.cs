using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;

//La relacion entre Estudiante y Curso es la Inscripcion
//creamos la inscripcion para guardar todos los datos que provengan del curso
// ejemplo: su asistencia, su nota, si esta activo o no

//Es solo de un estudiante a un curso
public class Inscripcion : Entity
{
    public Guid IdEstudiante { get; private set; }
    public Guid IdCurso { get; private set; }
    public DateTime FechaInscripcion { get; private set; }
    public bool Activa { get; private set; }
    private readonly List<Dictionary<Examen,double>> _historialNotas;
    private readonly List<Asistencia> _asistenciasDelEstudiante;
    public double porcentajeAsistencia { get; private set; }
    
    public Inscripcion() : base(Guid.NewGuid())
    {
        this._historialNotas = new List<Dictionary<Examen, double>>();
        this._asistenciasDelEstudiante = new List<Asistencia>();
    }
    
    public static Inscripcion CrearInscripcion(Guid idEstudiante, Guid idCurso)
    {
        if (idEstudiante == Guid.Empty)
            throw new ArgumentNullException(nameof(idEstudiante));
        
        if (idCurso == Guid.Empty)
            throw new ArgumentNullException(nameof(idCurso));
        
        Inscripcion inscripcion = new Inscripcion
        {
            IdEstudiante = idEstudiante,
            IdCurso = idCurso,
            FechaInscripcion = DateTime.UtcNow,
            Activa = true,
            porcentajeAsistencia = 0.0
        };
        
        return inscripcion;
    }

    public void ActualizarPorcentajeAsistencia()
    {
        int clasesTotal = _asistenciasDelEstudiante.Count;
        
        int clasespresentes = _asistenciasDelEstudiante.Count(a => a.Presente);

        this.porcentajeAsistencia = (double)clasespresentes / clasesTotal * 100;
    }
}