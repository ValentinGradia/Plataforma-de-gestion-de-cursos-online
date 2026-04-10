using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface IEstudianteRepository : IRepository<Estudiante>
{
    //Esto para obtener los estudiantes por una lista de ids
    Task<List<Estudiante>> ObtenerPorIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    
    Task ActualizarCursosActivosAInactivosAsync(Guid idInscripcion ,CancellationToken cancellationToken);
    
    Task<IEnumerable<Guid>> ObtenerCursosActivosDeEstudianteAsync(Guid idEstudiante, CancellationToken cancellationToken);
    
    Task<IEnumerable<Guid>> ObtenerHistorialCursosDeEstudianteAsync(Guid idEstudiante, CancellationToken cancellationToken);
}