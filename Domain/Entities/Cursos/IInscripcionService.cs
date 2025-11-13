using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

//creamos una interfaz para el servicio de inscripcion
public interface IInscripcionService
{
    void InscribirEstudiante(Estudiante estudiante, Curso curso);
    void DesinscribirEstudiante(Estudiante estudiante, Curso curso);
}