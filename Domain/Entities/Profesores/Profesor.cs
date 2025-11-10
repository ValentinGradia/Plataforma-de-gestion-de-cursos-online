using PlataformaDeGestionDeCursosOnline.Domain.Enum;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;

public sealed class Profesor : Usuario
{
    //private List<Curso> CursosQueEstaACargo;
    private string Especialidad;
    
    public Profesor(Guid id, 
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
}