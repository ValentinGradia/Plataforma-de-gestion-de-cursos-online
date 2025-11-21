using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

public class EntregaDelExamen : Entity
{
    public Guid IdEstudiante{ get; }
    public TipoExamen Tipo { get; }
    public string Respuesta { get; private set; }
    public Nota? Nota { get; private set; }
    public DateTime FechaEntrega { get; private set; }
    public string? ComentarioDocente { get; private set; }

    public EntregaDelExamen(Guid estudianteId, TipoExamen tipo , string respuesta) : base(Guid.NewGuid())
    {
        this.IdEstudiante = estudianteId;
        this.FechaEntrega = DateTime.UtcNow;
        this.Tipo = tipo;
        this.Respuesta = respuesta;
    }
    
    internal void AsignarNotaInterna(Nota nota)
    {   
        Nota = nota;
    }
    
    public void AgregarComentario(string comentario)
    {
        this.ComentarioDocente = comentario;
    }
    
}