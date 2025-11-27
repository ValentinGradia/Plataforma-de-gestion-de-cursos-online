using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Events;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

public class EntregaDelExamen : Entity
{
    private Guid IdExamen { get; }
    public Guid IdEstudiante{ get; }
    public TipoExamen Tipo { get; }
    public string Respuesta { get; private set; }
    public Nota? Nota { get; private set; }
    public DateTime FechaEntregado { get; private set; }
    public DateTime FechaLimiteExamen { get; private set; } 
    public string? ComentarioDocente { get; private set; }
    public bool FueEntregadoTarde => this.FechaEntregado > this.FechaLimiteExamen;

    private EntregaDelExamen(Guid idExamen, Guid estudianteId, TipoExamen tipo , string respuesta, DateTime fechaLimite) : base(Guid.NewGuid())
    {
        this.IdExamen = idExamen;
        this.IdEstudiante = estudianteId;
        this.FechaEntregado = DateTime.UtcNow;
        this.Tipo = tipo;
        this.Respuesta = respuesta;
        this.FechaLimiteExamen = fechaLimite;
    }
    
    public static Guid EntregarExamen(Guid idExamen, Guid idEstudiante, 
        TipoExamen tipo, string respuesta, DateTime fechaLimite)
    {
        EntregaDelExamen entrega =  new EntregaDelExamen(idExamen, idEstudiante, tipo, respuesta, fechaLimite);
        return entrega.Id;
    }
    
    public void AsignarNota(decimal valor)
    {
        Nota nota = new Nota(valor);
        this.RaiseDomainEvent(new NotaCargada(this.Id, DateTime.UtcNow));
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