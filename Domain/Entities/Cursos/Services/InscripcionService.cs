using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

//creamos este servicio para separar las responsabilidades de inscripcion
//y desinscripcion de estudiantes a los cursos
public class InscripcionService : IInscripcionService
{
    public void InscribirEstudiante(Estudiante estudiante, Curso curso)
    {
        
        curso.AgregarEstudiante(estudiante);
    }

    public void DesinscribirEstudiante(Estudiante estudiante, Curso curso)
    {
        curso.RemoverEstudiante(estudiante);
    }
}