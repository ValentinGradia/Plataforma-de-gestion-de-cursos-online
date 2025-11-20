using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;

public class Examen : Entity
{
    private Guid IdCurso;
    public string TemaExamen { get; private set; }
    public List<EntregaDelExamen?> EntregasDelExamen;
    public DateTime FechaLimiteDeEntrega { get; private set; }
    public DateTime FechaExamen { get; private set; }
    private readonly List<Nota> _notas = new List<Nota>();
    public IReadOnlyCollection<Nota> Notas => this._notas;
    public TipoExamen Tipo { get; private set; }

    private Examen(Guid idCurso, TipoExamen tipoExamen ,string temaExamen, DateTime fechaLimiteDeEntrega) : base(Guid.NewGuid())
    {
        this.IdCurso = idCurso;
        this.TemaExamen = temaExamen;
        this.FechaExamen = DateTime.UtcNow;
        this.Tipo = tipoExamen;
        this.FechaLimiteDeEntrega = fechaLimiteDeEntrega;
    }

    public static Examen CrearExamen(Guid idCurso, TipoExamen tipoExamen ,string temaExamen, DateTime fechaLimiteDeEntrega)
    {
        
        if (string.IsNullOrEmpty(temaExamen))
            throw new ArgumentNullException(nameof(temaExamen));

        var examen = new Examen(idCurso, tipoExamen ,temaExamen, fechaLimiteDeEntrega);
        return examen;
    }
    
    public void EntregarExamen(Guid idEstudiante, string respuesta)
    {
        bool entregadoFueraDeTiempo = DateTime.UtcNow > FechaLimiteDeEntrega;
        EntregaDelExamen entrega = new EntregaDelExamen(idEstudiante, this.Id, DateTime.UtcNow, respuesta, entregadoFueraDeTiempo);
        EntregasDelExamen.Add(entrega);
    }
    
    public void AsignarNota(Guid IdEstudiante, decimal valor)
    {
        Nota nota = new Nota(IdEstudiante, valor);
        _notas.Add(nota);
    }
}