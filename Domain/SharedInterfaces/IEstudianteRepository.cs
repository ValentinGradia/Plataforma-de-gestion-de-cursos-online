using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface IEstudianteRepository : IRepository<Estudiante>
{
    //Esto para obtener los estudiantes por una lista de ids
    Task<List<Estudiante>> ObtenerPorIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
}