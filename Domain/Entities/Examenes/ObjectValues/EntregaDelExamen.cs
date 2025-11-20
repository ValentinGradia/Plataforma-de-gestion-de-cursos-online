using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

public class EntregaDelExamen : Entity
{
    public Guid IdEstudiante{ get; }
    private Nota? Nota { get; }
    public DateTime FechaEntrega { get; private set; }
    public string? ComentarioDocente { get; private set; }

    public EntregaDelExamen(Guid estudianteId ) : base(Guid.NewGuid())
    {
        this.IdEstudiante = estudianteId;
        this.FechaEntrega = DateTime.UtcNow;
    }
    
    
}