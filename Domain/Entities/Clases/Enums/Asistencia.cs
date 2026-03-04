using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;

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
    internal Asistencia(Guid id,Guid idClase ,Guid idInscripcionEstudiante, bool presente) : base(id)
    {
        IdInscripcionEstudiante = idInscripcionEstudiante;
        Presente = presente;
    }

}
