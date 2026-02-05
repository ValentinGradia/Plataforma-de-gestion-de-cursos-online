namespace PlataformaDeGestionDeCursosOnline.Application.DTOs;

public class InscripcionDTO
{
    public Guid IdCurso { get; set; }
    public DateTime FechaInscripcion { get; set; }
    public bool Activa { get; set; }
}