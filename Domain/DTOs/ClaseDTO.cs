using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;

namespace PlataformaDeGestionDeCursosOnline.Application.DTOs;

public class ClaseDTO
{
    public string Material { get; set; }
    public DateTime Fecha { get; set; }
    public EstadoClase Estado { get; set; }
}