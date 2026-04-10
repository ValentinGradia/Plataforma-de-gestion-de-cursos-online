namespace PlataformaDeGestionDeCursosOnline.Domain.DTOs;

public class InscripcionDTO
{
    public Guid IdCurso { get; set; }
    public Guid IdInscripcion { get; set; }
    public DateTime FechaInscripcion { get; set; }
    public bool Activa { get; set; }
}