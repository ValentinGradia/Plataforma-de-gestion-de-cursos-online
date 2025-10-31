using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using PlataformaDeGestionDeCursosOnline.Domain.Enum;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain;

public abstract class Usuario
{
    protected Guid Id { get; init; } // -> el ID nunca cambia una vez que definimos la entidad
    
    protected Direccion Direccion { get; set; }
    
    protected Email Email { get; set; } 
    
    protected Contraseña Contraseña { get; init; }
    
    protected DNI Dni { get; set; }
    
    protected string Nombre { get; set; }
    
    protected string Apellido { get; set; }
    
    protected DateTime FechaRegistro { get; set; }
    
    public Roles Rol { get; protected set; }

    public virtual string ObtenerInformacion()
    {
        StringBuilder sb =  new StringBuilder();
        PropertyInfo[] properties =  this.GetType().GetProperties();
        foreach (PropertyInfo property in properties)
        {
            sb.Append($"{property.Name} : {property.GetValue(this)} \n");
        }

        return JsonSerializer.Serialize(sb.ToString(), new JsonSerializerOptions
        {
            WriteIndented = true 
        });
    }
}