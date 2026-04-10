using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.Events;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;

public class EntregaDelExamen : Entity
{
    public Guid IdExamen { get; private set; }
    //La inscripcion del estudiante al curso es unico y no se repite. Por eso almacenamos
    //el id de inscripcion del estudiante y no el id del estudiante.
    public Guid IdInscripcionEstudiante{ get; private set; }
    public TipoExamen Tipo { get; private set; }
    public string Respuesta { get; private set; }
    public Nota? Nota { get; private set; }
    public DateTime FechaEntregado { get; private set; }
    public string? ComentarioDocente { get; private set; }
    
    private EntregaDelExamen() : base() { }

    private EntregaDelExamen(Guid idExamen, Guid estudianteIdInscripcion, TipoExamen tipo , string respuesta) : base(Guid.NewGuid())
    {
        this.IdExamen = idExamen;
        this.IdInscripcionEstudiante = estudianteIdInscripcion;
        this.FechaEntregado = DateTime.UtcNow;
        this.Tipo = tipo;
        this.Respuesta = respuesta;
    }

    // Constructor interno para reconstrucción desde BD
    private EntregaDelExamen(Guid id, Guid idExamen, Guid estudianteIdInscripcion, TipoExamen tipo, string respuesta, DateTime fechaEntregado, Nota? nota, string? comentarioDocente) : base(id)
    {
        this.IdExamen = idExamen;
        this.IdInscripcionEstudiante = estudianteIdInscripcion;
        this.Tipo = tipo;
        this.Respuesta = respuesta;
        this.FechaEntregado = fechaEntregado;
        this.Nota = nota;
        this.ComentarioDocente = comentarioDocente;
    }
    
    public static EntregaDelExamen ReconstruirEntrega(Guid id, Guid idExamen, Guid estudianteIdInscripcion, TipoExamen tipo, string respuesta, DateTime fechaEntregado, Nota? nota, string? comentarioDocente)
    {
        return new EntregaDelExamen(id, idExamen, estudianteIdInscripcion, tipo, respuesta, fechaEntregado, nota, comentarioDocente);
    }

    public static EntregaDelExamen Crear(Guid idExamen, Guid IdInscripcionEstudiante, 
        TipoExamen tipo, string respuesta)
    {
        EntregaDelExamen entrega =  new EntregaDelExamen(idExamen,IdInscripcionEstudiante ,tipo, respuesta);
        return entrega;
    }
    
    public void AsignarNota(decimal valor)
    {
        Nota nota = new Nota(valor);
        this.RaiseDomainEvent(new NotaCargada(this.Id, this.IdExamen, this.IdInscripcionEstudiante));
        this.AsignarNotaInterna(nota);
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