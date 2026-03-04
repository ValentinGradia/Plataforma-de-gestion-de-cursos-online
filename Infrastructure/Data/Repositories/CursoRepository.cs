using System.Data;
using Dapper;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using static PlataformaDeGestionDeCursosOnline.Domain.Entities.Clase;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Data.Repositories;

public class CursoRepository(IDbConnectionFactory _connectionFactory) : ICursoRepository
{
    
    public async Task GuardarAsync(Curso curso)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "INSERT INTO Cursos (Id, Nombre, Temario, FechaInicio, FechaFin) VALUES (@Id, @Nombre, @Temario, @FechaInicio, @FechaFin)";
        await connection.ExecuteAsync(sql, new { Id = curso.Id, Nombre = curso.Nombre, Temario = curso.Temario, FechaInicio = curso.Duracion.Inicio, FechaFin = curso.Duracion.Fin });
    }

    public async Task ActualizarAsync(Curso clase, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE Cursos SET Nombre = @Nombre, Temario = @Temario WHERE Id = @Id";
        await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = clase.Id, Nombre = clase.Nombre, Temario = clase.Temario }, cancellationToken: cancellationToken));
    }

    public async Task<Curso?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        
        using var connection = _connectionFactory.CreateConnection();

        //query para el curso + profesor
        var sqlCurso = @"
            SELECT c.*, p.* from Cursos c LEFT JOIN Profesores p on p.Id == c.IdProfesor WHERE c.Id = @Id";

        //query para el curso + clases
        var sqlClases = @"
            SELECT * FROM Clases WHERE IdCurso = @Id";
        
        //query para el curso + examenes
        var sqlExamenes = @"
            SELECT * FROM Examenes WHERE IdCurso = @Id";
        
        //query para el curso + inscripciones + asistencias + entregas del examen
        var sqlInscripciones = @"
            SELECT i.*, a.*, e.* FROM Inscripciones i
            LEFT JOIN Asistencias a ON a.IdInscripcionEstudiante == i.Id
            LEFT JOIN EntregasDelExamen e on e.IdInscripcionEstudiante == i.Id
            WHERE i.IdCurso = @Id";
        
        var sql = sqlCurso + ";" + sqlClases + ";" + sqlExamenes + ";" + sqlInscripciones;

        //Ejecutamos las 4 queries en una sola llamada a la base de datos.
        using var multi = await connection.QueryMultipleAsync(sql, new { Id = id });

        var cursoRow        = await multi.ReadFirstOrDefaultAsync();//devuelve solo una fila, la del curso + profesor
        var clasesRows      = (await multi.ReadAsync()).ToList();
        var examenesRows    = (await multi.ReadAsync()).ToList();
        var inscripcionRows = (await multi.ReadAsync()).ToList();

        List<Clase> clases = clasesRows
            .Where(r => r.ClaseId != null)
            .Select(g =>
                {
                    var row = g.First();
                    return new Clase(row.ClaseId, row.ClaseCursoId, row.ClaseMaterial, row.ClaseFecha, row.ClaseEstado);
                }
            )
            .ToList<Clase>();
        
        List<Examen> examenes = examenesRows
            .Where(r => r.IdExamen != null)
            .Select(g =>
                {
                    var row = g.First();
                    return new Examen(row.IdExamen,row.IdCurso, row.TipoExamen, row.TemaExamen, row.FechaLimiteDeEntrega, row.FechaExamenCargado);
                }
            )
            .ToList<Examen>();
        
        List<Inscripcion> inscripciones = inscripcionRows
            .GroupBy(r => (Guid)r.Id)
            .Select(g =>
            {
                var first = g.First();
                var asistencias = g
                        .Where(r => r.AsistenciaId != null)
                        .Select(g =>
                        {
                            var row = g.First();
                            return new Asistencia(row.AsistenciaId, row.AsistenciaClaseId, row.AsistenciaPresente);
                        })
                    
                var entregas = g
                        .Where(r => r.EntregaId != null)
                        .Select(r => EntregaDelExamen.Reconstruir(...))
                    .ToList();

                return Inscripcion.Reconstruir(
                    id:                   (Guid)first.Id,
                    idEstudiante:         (Guid)first.EstudianteId,
                    idCurso:              (Guid)first.CursoId,
                    fechaInscripcion:     (DateTime)first.FechaInscripcion,
                    activa:               (bool)first.Activa,
                    porcentajeAsistencia: (double)first.PorcentajeAsistencia,
                    asistencias:          asistencias,
                    entregas:             entregas
                );
            })
            .ToList();
        


        Curso curso = new Curso(
            Id: row.Id,
            profesor: row.Profesor,
            temario: row.Temario,
            nombre: row.Nombre,
            estado: 0, // Placeholder, no mapeo completo
            duracion: null!, // Placeholder, no mapeo completo
            inscripciones: new List<Inscripcion>(), // Placeholder, no mapeo completo
            examenes: new List<Examen>(), // Placeholder, no mapeo completo
            clases: new List<Clase>() // Placeholder, no mapeo completo
        );)

        return curso;
    }

    public async Task<IEnumerable<Curso>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Cursos";
        var rows = await connection.QueryAsync(sql);
        return Enumerable.Empty<Curso>();
    }

    public async Task<Curso> ObtenerCursoPorIdModeloExamen(Guid IdModeloExamen, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT c.Id, c.Nombre, c.Temario FROM Cursos c JOIN ModelosExamen me ON me.CursoId = c.Id WHERE me.Id = @IdModeloExamen";
        var row = await connection.QuerySingleOrDefaultAsync(new CommandDefinition(sql, new { IdModeloExamen }, cancellationToken: cancellationToken));
        // Placeholder: no mapeo completo
        return null!;
    }

    public async Task<Clase?> ObtenerClasePorId(Guid IdClase, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Id, IdCurso, Material, Fecha FROM Clases WHERE Id = @Id";
        var clase = await connection.QuerySingleOrDefaultAsync<Clase>(new CommandDefinition(sql, new { Id = IdClase }, cancellationToken: cancellationToken));
        return clase;
    }

    public async Task<List<Estudiante>> ObtenerEstudiantesInscriptosEnCurso(Guid IdCurso, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"SELECT e.* FROM Estudiantes e
JOIN Inscripciones i ON i.EstudianteId = e.Id
WHERE i.CursoId = @IdCurso";
        var estudiantes = await connection.QueryAsync<Estudiante>(new CommandDefinition(sql, new { IdCurso }, cancellationToken: cancellationToken));
        return estudiantes.ToList();
    }
}
