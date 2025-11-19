using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examen;

public class Examen
{
    public Guid IdCurso;
    public string TemaExamen;
    public DateTime FechaExamen;
    private readonly List<Nota> _notas;
    private TipoExamen Tipo;

    private Examen(Guid idCurso, TipoExamen tipoExamen ,string temaExamen)
    {
        this.IdCurso = idCurso;
        this.TemaExamen = temaExamen;
        this.FechaExamen = DateTime.UtcNow;
        this.Tipo = tipoExamen;
    }

    public static Examen CrearExamen(Guid idCurso, TipoExamen tipoExamen ,string temaExamen)
    {
        
        if (string.IsNullOrEmpty(temaExamen))
            throw new ArgumentNullException(nameof(temaExamen));

        var examen = new Examen(idCurso, tipoExamen ,temaExamen);
        return examen;
    }
    
    
}