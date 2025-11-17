using System.Runtime.CompilerServices;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Enum;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;

public sealed class Profesor : Usuario
{
    private List<Curso> CursosQueEstaACargo;
    private string Especialidad;
    
    private Profesor(Guid id, 
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
        Roles rol) : base(id, pais, ciudad, calle, altura, email, contraseña, dni, nombre, apellido, rol)
    {
        this.Especialidad = especialidad;
    }

    public static Profesor CrearProfesor(Guid id,
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
        
        return new Profesor(id, pais, ciudad, calle, altura, email, contraseña, dni, nombre, apellido, especialidad, Roles.Profesor);
    }
    
    public void CambiarEspecialidad(string nuevaEspecialidad)
    {
        if (string.IsNullOrEmpty(nuevaEspecialidad))
            throw new ArgumentNullException(nameof(nuevaEspecialidad));
        
        this.Especialidad = nuevaEspecialidad;
    }
}