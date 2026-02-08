using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface IInscripcionRepository : IRepository<Inscripcion>
{
    Task<Inscripcion?> ObtenerEstudiantePorIdAsync(Guid idEstudiante, CancellationToken cancellationToken = default);
}