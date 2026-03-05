using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

public class EntregaDelExamen : Entity
{
    private Guid IdExamen { get; }
    //La inscripcion del estudiante al curso es unico y no se repite. Por eso almacenamos
    //el id de inscripcion del estudiante y no el id del estudiante.
    public Guid IdInscripcionEstudiante{ get; private set; }
    public TipoExamen Tipo { get; private set; }
    public string Respuesta { get; private set; }
    public Nota? Nota { get; private set; }
    public DateTime FechaEntregado { get; private set; }
    public DateTime FechaLimiteExamen { get; private set; } 
    public string? ComentarioDocente { get; private set; }
    public bool FueEntregadoTarde => this.FechaEntregado > this.FechaLimiteExamen;

    private EntregaDelExamen(Guid idExamen, Guid estudianteIdInscripcion, TipoExamen tipo , string respuesta, DateTime fechaLimite) : base(Guid.NewGuid())
    {
        this.IdExamen = idExamen;
        this.IdInscripcionEstudiante = estudianteIdInscripcion;
        this.FechaEntregado = DateTime.UtcNow;
        this.Tipo = tipo;
        this.Respuesta = respuesta;
        this.FechaLimiteExamen = fechaLimite;
    }

    // Constructor interno para reconstrucción desde BD
    private EntregaDelExamen(Guid id, Guid idExamen, Guid estudianteIdInscripcion, TipoExamen tipo, string respuesta, DateTime fechaEntregado, DateTime fechaLimite, Nota? nota, string? comentarioDocente) : base(id)
    {
        this.IdExamen = idExamen;
        this.IdInscripcionEstudiante = estudianteIdInscripcion;
        this.Tipo = tipo;
        this.Respuesta = respuesta;
        this.FechaEntregado = fechaEntregado;
        this.FechaLimiteExamen = fechaLimite;
        this.Nota = nota;
        this.ComentarioDocente = comentarioDocente;
    }
    
    public static EntregaDelExamen ReconstruirEntrega(Guid id, Guid idExamen, Guid estudianteIdInscripcion, TipoExamen tipo, string respuesta, DateTime fechaEntregado, DateTime fechaLimite, Nota? nota, string? comentarioDocente)
    {
        return new EntregaDelExamen(id, idExamen, estudianteIdInscripcion, tipo, respuesta, fechaEntregado, fechaLimite, nota, comentarioDocente);
    }

    public static EntregaDelExamen Crear(Guid idExamen, Guid IdInscripcionEstudiante, 
        TipoExamen tipo, string respuesta, DateTime fechaLimite)
    {
        EntregaDelExamen entrega =  new EntregaDelExamen(idExamen,IdInscripcionEstudiante ,tipo, respuesta, fechaLimite);
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