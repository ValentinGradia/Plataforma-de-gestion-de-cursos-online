using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface ICursoRepository : IRepository<Curso>
{
    Task<Curso> ObtenerCursoPorIdModeloExamen(Guid IdModeloExamen, CancellationToken cancellationToken);
    
}