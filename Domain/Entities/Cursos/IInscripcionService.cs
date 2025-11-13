namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

public interface IInscripcionService
{
    void InscribirEstudiante(Guid estudianteId, Curso curso);
    
    void DesinscribirEstudiante(Guid estudianteId, Curso curso);
}