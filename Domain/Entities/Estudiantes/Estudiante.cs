using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Enum;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

public class Estudiante : Usuario 
{
    
    private List<Curso> cursosInscritosActualmente = new();
    private List<Curso> historialDeCursos = new();

    public Estudiante(string pais, string ciudad, string calle, int altura, string email, string contraseña, string dni, string nombre, string apellido, Roles rol, Guid idUsuario) : base(pais, ciudad, calle, altura, email, contraseña, dni, nombre, apellido, rol)
    {
        
    }

    

}