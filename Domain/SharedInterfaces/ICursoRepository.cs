using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface ICursoRepository : IRepository<Curso>
{
    Task<Curso> ObtenerCursoPorIdModeloExamen(Guid IdModeloExamen, CancellationToken cancellationToken);
    
    Task<Clase?> ObtenerClasePorId(Guid IdClase, CancellationToken cancellationToken);
    
    // Hacemos un override sobre el obtener todos, debido a que el devolver el objetoc curso entero con todas las entidades es muy pesado,
    // entonces devolvemos un DTO con la informacion basica del curso para mostrar en el listado de cursos.
    new Task<IEnumerable<CursoDTO>> ObtenerTodosAsync();
    
    Task<List<Estudiante>> ObtenerEstudiantesInscriptosEnCurso(Guid IdCurso, CancellationToken cancellationToken);
    
}