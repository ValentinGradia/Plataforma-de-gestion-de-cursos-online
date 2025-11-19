using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examen;

public class Examen
{
    public Guid IdCurso;
    public string temaExamen;
    public DateTime fechaExamen;
    private Nota notaExamenAlumno;
    private TipoExamen tipoExamen;

    private Examen(Guid idCurso, TipoExamen tipoExamen ,string temaExamen)
    {
        this.IdCurso = idCurso;
        this.temaExamen = temaExamen;
        this.fechaExamen = DateTime.UtcNow;
        this.tipoExamen = tipoExamen;
    }

    public static Examen CrearExamen(Guid idCurso, TipoExamen tipoExamen ,string temaExamen)
    {
        
        if (string.IsNullOrEmpty(temaExamen))
            throw new ArgumentNullException(nameof(temaExamen));

        var examen = new Examen(idCurso, tipoExamen ,temaExamen);
        return examen;
    }
}