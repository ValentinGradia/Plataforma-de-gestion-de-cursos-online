using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Interfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Services;

//creamos este servicio para separar las responsabilidades de inscripcion
//y desinscripcion de estudiantes a los cursos
public class InscripcionService : IInscripcionService
{
    public void InscribirEstudiante(Inscripcion estudiante, Curso curso)
    {
        curso.AgregarEstudiante(estudiante);
    }

    public void DesinscribirEstudiante(Inscripcion estudiante, Curso curso)
    {
        curso.RemoverEstudiante(estudiante);
    }
}