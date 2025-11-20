using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examen;

public class Examen
{
    private Guid IdCurso;
    public string TemaExamen;
    public DateTime FechaExamen;
    private readonly List<Nota> _notas = new List<Nota>();
    public IReadOnlyCollection<Nota> Notas => this._notas;
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
    
    public void AsignarNota(Guid IdEstudiante, decimal valor)
    {
        var nota = new Nota(IdEstudiante, valor);
        _notas.Add(nota);
    }
}