using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface IEstudianteRepository : IRepository<Estudiante>
{
    //Esto para obtener los estudiantes por una lista de ids
    Task<List<Estudiante>> ObtenerPorIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

    // Actualiza la tabla EstudianteCursosActivos sincronizando los cursos activos del estudiante
    Task ActualizarCursosActivosAsync(Estudiante estudiante, CancellationToken cancellationToken);

    // Actualiza la tabla EstudianteHistorialCursos sincronizando el historial de cursos del estudiante
    Task ActualizarHistorialCursosAsync(Estudiante estudiante, CancellationToken cancellationToken);
    
    Task<IEnumerable<Guid>> ObtenerCursosActivosDeEstudianteAsync(Guid idEstudiante, CancellationToken cancellationToken);
    
    Task<IEnumerable<Guid>> ObtenerHistorialCursosDeEstudianteAsync(Guid idEstudiante, CancellationToken cancellationToken);
}