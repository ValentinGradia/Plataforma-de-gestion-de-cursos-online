using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Enum;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain;

public abstract class Usuario : Entity
{
    
    protected Direccion Direccion { get; set; }
    
    protected Email Email { get; set; } 
    
    protected Contraseña Contraseña { get; set; }
    
    protected DNI Dni { get; set; }
    
    protected string Nombre { get; set; }
    
    protected string Apellido { get; set; }
    
    protected DateTime FechaRegistro { get; init; }
    
    protected Roles Rol { get; set; }
    
    protected Usuario(
        Guid id,
        string pais,
        string ciudad,
        string calle,
        int altura,
        string email,
        string contraseña,
        string dni,
        string nombre,
        string apellido,
        Roles rol) : base(id)
    {
        this.Direccion = Direccion.CrearDireccion(pais, ciudad, calle, altura);
        this.Email = Email.CrearEmail(email);
        this.Contraseña = Contraseña.CrearContraseña(contraseña);
        this.Dni = DNI.CrearDNI(dni);
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.FechaRegistro = DateTime.Now;
        this.Rol = rol;
    }
    
    public void CambiarContraseña(string nuevaContraseña)
    {
        this.Contraseña = Contraseña.CrearContraseña(nuevaContraseña);
    }

    public void CambiarPais(string pais)
    {
        this.Direccion.CambiarPais(pais);
    }
    
    public void CambiarCiudad(string ciudad)
    {
        this.Direccion.CambiarCiudad(ciudad);
    }
    
    public void CambiarCalle(string calle)
    {
        this.Direccion.CambiarCalle(calle);
    }
    
    public void CambiarAltura(int altura)
    {
        this.Direccion.CambiarAltura(altura);
    }

    public void CambiarDni(string nuevoDni)
    {
        this.Dni = DNI.CrearDNI(nuevoDni);
    }
    
    public void CambiarEmail(string email)
    {
        this.Email = Email.CrearEmail(email);
    }

    public void CambiarNombre(string nuevoNombre)
    {
        this.Nombre = nuevoNombre;
    }

    public void CambiarApellido(string nuevoApellido)
    {
        this.Apellido = nuevoApellido;
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