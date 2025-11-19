using System.Runtime.CompilerServices;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;

public class Examen : Entity
{
    public Guid IdCurso;
    public string temaExamen;
    public DateTime fechaExamen;
    private Nota notaExamen;
    private TipoExamen tipoExamen;

    private Examen(Guid idCurso, TipoExamen tipoExamen ,string temaExamen) : base(Guid.NewGuid())
    {
        this.IdCurso = idCurso;
        this.temaExamen = temaExamen;
        this.fechaExamen = DateTime.UtcNow;
        this.tipoExamen = tipoExamen;
    }

    public static Examen CrearNota(Guid idCurso, TipoExamen tipoExamen ,string temaExamen)
    {
        
        if (string.IsNullOrEmpty(temaExamen))
            throw new ArgumentNullException(nameof(temaExamen));

        var examen = new Examen(idCurso, tipoExamen ,temaExamen);
        return examen;
    }
}