using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases.Enums;

namespace PlataformaDeGestionDeCursosOnline.Domain.DTOs;

public class ClaseDTO
{
    public string Material { get; set; }
    public DateTime Fecha { get; set; }
    public EstadoClase Estado { get; set; }
}