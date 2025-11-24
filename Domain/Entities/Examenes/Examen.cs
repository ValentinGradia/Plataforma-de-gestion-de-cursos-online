using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;

public class Examen : Entity
{
    private Guid IdCurso;
    public string TemaExamen { get; private set; }
    // private readonly List<EntregaDelExamen?> _entregasDelExamen;
    public DateTime FechaLimiteDeEntrega { get; private set; }
    public DateTime FechaExamenCargado { get; private set; }
    public TipoExamen Tipo { get; private set; }

    private Examen(Guid idCurso ,TipoExamen tipoExamen ,string temaExamen, DateTime fechaLimiteDeEntrega) : base(Guid.NewGuid())
    {
        this.IdCurso = idCurso;
        this.TemaExamen = temaExamen;
        this.FechaExamenCargado = DateTime.UtcNow;
        this.Tipo = tipoExamen;
        this.FechaLimiteDeEntrega = fechaLimiteDeEntrega;
    }

    public static Examen CrearExamen(Guid idCurso, TipoExamen tipoExamen ,string temaExamen, DateTime fechaLimiteDeEntrega)
    {
        if (string.IsNullOrEmpty(temaExamen))
            throw new ArgumentNullException(nameof(temaExamen));

        var examen = new Examen(idCurso,tipoExamen ,temaExamen, fechaLimiteDeEntrega);
        return examen;
    }
    
}