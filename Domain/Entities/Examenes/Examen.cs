using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;

public class Examen : Entity
{
    public string TemaExamen { get; private set; }
    private readonly List<EntregaDelExamen?> _entregasDelExamen;
    public DateTime FechaLimiteDeEntrega { get; private set; }
    public DateTime FechaExamen { get; private set; }
    public TipoExamen Tipo { get; private set; }

    private Examen(TipoExamen tipoExamen ,string temaExamen, DateTime fechaLimiteDeEntrega) : base(Guid.NewGuid())
    {
        this.TemaExamen = temaExamen;
        this.FechaExamen = DateTime.UtcNow;
        this.Tipo = tipoExamen;
        this.FechaLimiteDeEntrega = fechaLimiteDeEntrega;
    }

    public static Examen CrearExamen(Guid idCurso, TipoExamen tipoExamen ,string temaExamen, DateTime fechaLimiteDeEntrega)
    {
        
        if (string.IsNullOrEmpty(temaExamen))
            throw new ArgumentNullException(nameof(temaExamen));

        var examen = new Examen( tipoExamen ,temaExamen, fechaLimiteDeEntrega);
        return examen;
    }
    
    public void EntregarExamen(Guid idEstudiante, string respuesta, TipoExamen tipo)
    {
        bool entregadoFueraDeTiempo = DateTime.UtcNow > FechaLimiteDeEntrega;
        EntregaDelExamen entrega = new EntregaDelExamen(idEstudiante, tipo ,respuesta);
        _entregasDelExamen.Add(entrega);
    }
    
    public void AsignarNota(Guid idEstudiante, decimal valor)
    {
        EntregaDelExamen entrega = _entregasDelExamen.FirstOrDefault(e => e != null && e.IdEstudiante == idEstudiante)
            ?? throw new InvalidOperationException("El estudiante no ha entregado el examen.");
        
        Nota nota = new Nota(valor);
        entrega.AsignarNotaInterna(nota);
    }
}