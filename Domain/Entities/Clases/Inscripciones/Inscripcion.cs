using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;

//La relacion entre Estudiante y Curso es la Inscripcion
//creamos la inscripcion para guardar todos los datos que provengan del curso
// ejemplo: su asistencia, su nota, si esta activo o no
public class Inscripcion : Entity
{
    public Guid IdEstudiante { get; private set; }
    public Guid IdCurso { get; private set; }
    public DateTime FechaInscripcion { get; private set; }
    public bool Activa { get; private set; }
    private List<Dictionary<TipoExamen, double>> historialNotas;
    
    
    public Inscripcion() : base(Guid.NewGuid())
    {
    }
}