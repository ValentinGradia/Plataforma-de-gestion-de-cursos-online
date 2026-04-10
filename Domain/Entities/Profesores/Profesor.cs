using System.Runtime.CompilerServices;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain.Enum;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;

public sealed class Profesor : Usuario
{
    private List<Guid> CursosQueEstaACargo = new();
    public string Especialidad { get; private set; }
    
    private Profesor( 
        string pais,
        string ciudad,
        string calle,
        int altura,
        string email,
        string contraseña,
        string dni,
        string nombre,
        string apellido,
        string especialidad,
        Roles rol) : base(pais, ciudad, calle, altura, email, contraseña, dni, nombre, apellido, rol)
    {
        this.Especialidad = especialidad;
    }

    // Constructor interno para reconstrucción desde BD
    internal Profesor(Guid id, Direccion direccion, Email email, Contraseña contraseña, DNI dni, string nombre, string apellido, DateTime fechaRegistro, string especialidad) : base(id, direccion, email, contraseña, dni, nombre, apellido, fechaRegistro, Roles.Profesor)
    {
        this.Especialidad = especialidad;
    }

    public static Profesor Reconstruir(
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
        DateTime fechaRegistro,
        string especialidad)
    {
        return new Profesor(
            id,
            Direccion.CrearDireccion(pais, ciudad, calle, altura),
            Email.CrearEmail(email),
            Contraseña.CrearContraseña(contraseña),
            DNI.CrearDNI(dni),
            nombre,
            apellido,
            fechaRegistro,
            especialidad
        );
    }

    public static Profesor CrearProfesor(
        string pais,
        string ciudad,
        string calle,
        int altura,
        string email,
        string contraseña,
        string dni,
        string nombre,
        string apellido,
        string especialidad)
    {
        //validaciones de negocio
        if (string.IsNullOrEmpty(especialidad))
            throw new ArgumentNullException(nameof(especialidad));
        
        return new Profesor( pais, ciudad, calle, altura, email, contraseña, dni, nombre, apellido, especialidad, Roles.Profesor);
    }
    
    public void CambiarEspecialidad(string nuevaEspecialidad)
    {
        if (string.IsNullOrEmpty(nuevaEspecialidad))
            throw new ArgumentNullException(nameof(nuevaEspecialidad));
        
        this.Especialidad = nuevaEspecialidad;
    }

    public void AgregarCursoACargo(Guid idCurso)
    {
        if (!CursosQueEstaACargo.Contains(idCurso))
            CursosQueEstaACargo.Add(idCurso);
    }

    public void EliminarCursoACargo(Guid idCurso)
    {
        CursosQueEstaACargo.Remove(idCurso);
    }
    
}