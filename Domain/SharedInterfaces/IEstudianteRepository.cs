using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface IEstudianteRepository : IRepository<Estudiante>
{
    Task<List<Estudiante>> ObtenerPorIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);
}