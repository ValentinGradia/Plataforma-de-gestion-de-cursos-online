using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Interfaces;

//creamos una interfaz para el servicio de inscripcion
public interface IInscripcionService
{
    void InscribirEstudiante(Inscripcion estudiante, Curso curso);
    void DesinscribirEstudiante(Inscripcion estudiante, Curso curso);
}