using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases;

public class Asistencia : Entity
{
    public Guid IdInscripcionEstudiante;
    public Guid IdClase;

    public bool Presente;
    
    public Asistencia(Guid idClase,Guid idInscripcionEstudiante, bool presente) : base(Guid.NewGuid())
    {
        IdClase = idClase;
        IdInscripcionEstudiante = idInscripcionEstudiante;
        Presente = presente;
    }
    
    // Constructor interno para reconstrucción desde BD
    private Asistencia(Guid id,Guid idClase ,Guid idInscripcionEstudiante, bool presente) : base(id)
    {
        IdInscripcionEstudiante = idInscripcionEstudiante;
        Presente = presente;
        IdClase = idClase;
    }
    
    public static Asistencia ReconstruirAsistencia(Guid id, Guid idClase, Guid idInscripcionEstudiante, bool presente)
    {
        return new Asistencia(id, idClase, idInscripcionEstudiante, presente);
    }

}
