namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;

public sealed record DateRange
{
    public DateTime Inicio { get; init; }
    public DateTime Fin { get; init; }
    
    public int CantidadDias => (Fin - Inicio).Days;
    
    private DateRange(DateTime inicio, DateTime fin)
    {
        this.Inicio = inicio;
        this.Fin = fin;
    }
    
    public static DateRange CrearDateRange(DateTime inicio, DateTime fin)
    {
        if (fin < inicio)
            throw new ArgumentException("La fecha de fin no puede ser anterior a la fecha de inicio.");

        return new DateRange(inicio, fin);
    }
}