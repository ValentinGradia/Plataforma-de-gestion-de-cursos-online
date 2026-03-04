using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain.Enum;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

public class Estudiante : Usuario 
{
    
    private List<Curso?> cursosInscritosActualmente;
    private List<Curso?> historialDeCursos;

    public Estudiante(string pais, string ciudad, string calle, int altura, string email, string contraseña, string dni, string nombre, string apellido) : base(pais, ciudad, calle, altura, email, contraseña, dni, nombre, apellido, Roles.Estudiante)
    {
        
    }

    // Constructor interno para reconstrucción desde BD
    internal Estudiante(Guid id, Direccion direccion, Email email, Contraseña contraseña, DNI dni, string nombre, string apellido, DateTime fechaRegistro) : base(id, direccion, email, contraseña, dni, nombre, apellido, fechaRegistro, Roles.Estudiante)
    {
    }

}