using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface ICursoRepository : IRepository<Curso>
{
    Task<Curso> ObtenerCursoPorIdModeloExamen(Guid IdModeloExamen, CancellationToken cancellationToken);
    
    Task<Clase?> ObtenerClasePorId(Guid IdClase, CancellationToken cancellationToken);
    
    Task<CursoDTO> ObtenerTodosLosCursosDTO();
    
    Task<List<Estudiante>> ObtenerEstudiantesInscriptosEnCurso(Guid IdCurso, CancellationToken cancellationToken);
    
}