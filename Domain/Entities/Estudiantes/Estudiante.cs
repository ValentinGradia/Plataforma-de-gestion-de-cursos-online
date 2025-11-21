using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Enum;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

public class Estudiante : Usuario 
{
    
    private List<Curso?> cursosInscritosActualmente;
    private List<Curso?> historialDeCursos;

    public Estudiante(string pais, string ciudad, string calle, int altura, string email, string contraseña, string dni, string nombre, string apellido) : base(pais, ciudad, calle, altura, email, contraseña, dni, nombre, apellido, Roles.Estudiante)
    {
        
    }

    

}