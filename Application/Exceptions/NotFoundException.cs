namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("La entidad no fue encontrada") { }
}