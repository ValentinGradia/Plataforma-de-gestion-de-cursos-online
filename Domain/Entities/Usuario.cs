using PlataformaDeGestionDeCursosOnline.Domain.Enum;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain;

public abstract class Usuario
{
    public Guid Id { get; protected init; } // -> el ID nunca cambia una vez que definimos la entidad
    
    public Direccion Direccion { get; protected set; }
    
    public DNI Dni { get; protected set; }
    
    public string Nombre { get; protected set; }
    
    public string Apellido { get; protected set; }
    
    public DateTime FechaRegistro { get; protected set; }
    
    public Roles Rol { get; protected set; }
}