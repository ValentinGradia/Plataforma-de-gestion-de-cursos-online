using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;

public class Examen : Entity
{
    public Guid idCurso;
    public string temaExamen;
    public DateTime fechaExamen;
    private Nota notaExamen;
    
    public Examen(Guid id) : base(id)
    {
    }
}