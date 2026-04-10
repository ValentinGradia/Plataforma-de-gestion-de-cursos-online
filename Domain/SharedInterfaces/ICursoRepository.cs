using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface ICursoRepository : IRepository<Curso>
{
    Task<Curso> ObtenerCursoPorIdModeloExamen(Guid IdModeloExamen, CancellationToken cancellationToken);

    Task<Clase?> ObtenerClasePorId(Guid IdClase, CancellationToken cancellationToken);
    
    Task InsertarClaseAsync(Clase clase, CancellationToken cancellationToken);
    Task ActualizarClaseAsync(Clase clase, CancellationToken cancellationToken);

    Task<Examen?> ObtenerExamenPorIdAsync(Guid idExamen, CancellationToken cancellationToken);
    Task InsertarExamenAsync(Examen examen, CancellationToken cancellationToken);
    Task ActualizarExamenAsync(Examen examen, CancellationToken cancellationToken);
    Task CrearInscripcionAsync(Inscripcion inscripcion, CancellationToken cancellationToken);
    Task ActualizarInscripcionAsync(Inscripcion inscripcion, CancellationToken cancellationToken);

    Task<EntregaDelExamen?> ObtenerEntregaExamenPorIdAsync(Guid idEntrega, CancellationToken cancellationToken);
    Task InsertarEntregaExamenAsync(EntregaDelExamen entrega, Guid idExamen, CancellationToken cancellationToken);
    Task ActualizarEntregaExamenAsync(EntregaDelExamen entrega, CancellationToken cancellationToken);

    Task<Consulta?> ObtenerConsultaPorIdAsync(Guid idConsulta, CancellationToken cancellationToken);
    Task InsertarConsultaAsync(Consulta consulta, CancellationToken cancellationToken);
    Task ActualizarConsultaAsync(Consulta consulta, CancellationToken cancellationToken);

    // Hacemos un override sobre el obtener todos, debido a que el devolver el objetoc curso entero con todas las entidades es muy pesado,
    // entonces devolvemos un DTO con la informacion basica del curso para mostrar en el listado de cursos.
    new Task<IEnumerable<CursoDTO>> ObtenerTodosAsync();

    Task<List<Estudiante>> ObtenerEstudiantesInscriptosEnCurso(Guid IdCurso, CancellationToken cancellationToken);

    Task<Inscripcion?> ObtenerInscripcionPorIdEstudianteYCursoAsync(Guid idEstudiante, Guid idCurso, CancellationToken cancellationToken);
    Task<List<EntregaDelExamen>> ObtenerEntregasDeExamenesPorIdInscripcionAsync(Guid idInscripcionEstudiante, CancellationToken cancellationToken);
}