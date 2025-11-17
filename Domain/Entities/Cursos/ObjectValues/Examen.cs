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

    private Examen(Guid id, Guid idCurso, string temaExamen) : base(id)
    {
        this.IdCurso = idCurso;
        this.temaExamen = temaExamen;
        this.fechaExamen = DateTime.UtcNow;
    }



    public static Examen Create(Guid idCurso, string temaExamen)
    {
        
        if (string.IsNullOrEmpty(temaExamen))
            throw new ArgumentNullException(nameof(temaExamen));

        var examen = new Examen(Guid.NewGuid(), idCurso, temaExamen);
        return examen;
    }
}