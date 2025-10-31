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
    
    protected Roles Rol { get; set; }
    
    protected Usuario(
        Guid id,
        Direccion direccion,
        Email email,
        Contraseña contraseña,
        DNI dni,
        string nombre,
        string apellido,
        DateTime fechaRegistro,
        Roles rol)
    {
        this.Id = id;
        this.Direccion = direccion;
        this.Email = email;
        this.Contraseña = contraseña;
        this.Dni = dni;
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.FechaRegistro = fechaRegistro;
        this.Rol = rol;
    }
    
    public virtual string ObtenerInformacionDeMiUsuario()
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

    // public static string ObtenerInformacionDeUnUsuario(Guid id)
    // {    
    //     
    // }
    //
    // public static string ObtenerInformacionDeTodosUsuarios()
    // {
    //     
    // }
}