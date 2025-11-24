namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas.ObjectValues;

public record Calificacion
{
    public int Valor { get; }

    public Calificacion(int valor)
    {
        ValidarCalificacion(valor, nameof(valor));

        this.Valor = valor;
    }
    
    private static void ValidarCalificacion(int valor, string nombreParametro)
    {
        if (valor < 1 || valor > 10)
            throw new ArgumentOutOfRangeException(nombreParametro, "La calificación debe estar entre 1 y 10.");
    }
}