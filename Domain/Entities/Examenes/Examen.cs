using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;

//Este seria el modelo del examen, luego el estudiante sube su propio examen para ser corregido.
public class Examen : Entity
{
    public Guid IdCurso { get; private set; }
    public string TemaExamen { get; private set; }
    // private readonly List<EntregaDelExamen?> _entregasDelExamen;
    public DateTime FechaLimiteDeEntrega { get; private set; }
    public DateTime FechaExamenCargado { get; private set; }
    public TipoExamen Tipo { get; private set; }
    
    private Examen() : base() {}

    private Examen(Guid idCurso ,TipoExamen tipoExamen ,string temaExamen, DateTime fechaLimiteDeEntrega) : base(Guid.NewGuid())
    {
        this.IdCurso = idCurso;
        this.TemaExamen = temaExamen;
        this.FechaExamenCargado = DateTime.UtcNow;
        this.Tipo = tipoExamen;
        this.FechaLimiteDeEntrega = fechaLimiteDeEntrega;
    }

    // Constructor interno para reconstrucción desde BD
    private Examen(Guid id, Guid idCurso, TipoExamen tipoExamen, string temaExamen, DateTime fechaLimiteDeEntrega, DateTime fechaExamenCargado) : base(id)
    {
        this.IdCurso = idCurso;
        this.TemaExamen = temaExamen;
        this.FechaExamenCargado = fechaExamenCargado;
        this.Tipo = tipoExamen;
        this.FechaLimiteDeEntrega = fechaLimiteDeEntrega;
    }
    
    public static Examen ReconstruirExamen(Guid id, Guid idCurso, TipoExamen tipoExamen, string temaExamen, DateTime fechaLimiteDeEntrega, DateTime fechaExamenCargado)
    {
        return new Examen(id, idCurso, tipoExamen, temaExamen, fechaLimiteDeEntrega, fechaExamenCargado);
    }

    public static Examen CrearExamen(Guid idCurso, TipoExamen tipoExamen ,string temaExamen, DateTime fechaLimiteDeEntrega)
    {
        if (string.IsNullOrEmpty(temaExamen))
            throw new ArgumentNullException(nameof(temaExamen));

        Examen examen = new Examen(idCurso,tipoExamen ,temaExamen, fechaLimiteDeEntrega);
        return examen;
    }
    
}