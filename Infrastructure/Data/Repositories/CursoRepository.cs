using System.Data;
using Dapper;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;
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
            SELECT 
                c.Id,
                c.Nombre,
                c.Temario,
                c.Estado,
                c.FechaInicio,
                c.FechaFin,
                p.Id AS IdProfesor
            FROM Cursos c 
            LEFT JOIN Profesores p ON p.Id = c.IdProfesor 
            WHERE c.Id = @Id";

        //query para el curso + clases
        var sqlClases = @"
            SELECT * FROM Clases WHERE IdCurso = @Id";
        
        //query para el curso + examenes
        var sqlExamenes = @"
            SELECT * FROM Examenes WHERE IdCurso = @Id";
        
        //query para el curso + inscripciones + asistencias + entregas del examen
        var sqlInscripciones = @"
            SELECT 
                i.Id                        AS IdInscripcion,
                i.IdCurso                  ,
                i.IdEstudiante             AS IdInscripcionEstudiante,
                i.FechaInscripcion          ,
                i.Activa                    ,
                i.PorcentajeAsistencia      ,

                a.Id                        AS IdAsistencia,
                a.IdInscripcionEstudiante   AS IdAsistenciaInscripcionEstudiante,
                a.IdClase                   AS IdAsistencaClase,
                a.Presente                  AS AsistenciaPresente,

                e.Id                        AS IdEntrega,
                e.IdExamen                  ,
                e.IdInscripcionEstudiante   AS IdEntregaInscripcionEstudiante,
                e.TipoExamen                AS TipoEntregaExamen,
                e.Respuesta                 AS EntregaExamenRespuesta,
                e.FechaEntrega              AS EntregaFechaEntrega,
                e.FechaLimiteExamen         AS EntregaFechaLimiteExamen,
                e.ComentarioDocente         AS EntregaComentarioDocente,
                e.ValorNota                 AS EntregaNota

            FROM Inscripciones i
            LEFT JOIN Asistencias a         ON a.IdInscripcionEstudiante = i.Id
            LEFT JOIN EntregasDelExamen e   ON e.IdInscripcionEstudiante = i.Id
            WHERE i.IdCurso = @Id";
        
        var sql = sqlCurso + ";" + sqlClases + ";" + sqlExamenes + ";" + sqlInscripciones;

        //Ejecutamos las 4 queries en una sola llamada a la base de datos.
        using var multi = await connection.QueryMultipleAsync(sql, new { Id = id });

        var cursoRow        = await multi.ReadFirstOrDefaultAsync();//devuelve solo una fila, la del curso + profesor
        var clasesRows      = (await multi.ReadAsync()).ToList();
        var examenesRows    = (await multi.ReadAsync()).ToList();
        var inscripcionRows = (await multi.ReadAsync()).ToList();

        List<Clase> clases = clasesRows
            .Where(r => r.Id != null)
            .Select(r => Clase.ReconstruirClase(
                id:       (Guid)r.Id,
                idCurso:  (Guid)r.IdCurso,
                material: (string)r.Material,
                fecha:    (DateTime)r.Fecha,
                estado:   (EstadoClase)r.Estado
            ))
            .ToList();
        
        List<Examen> examenes = examenesRows
            .Where(r => r.Id != null)
            .Select(row => Examen.ReconstruirExamen(
                    id: (Guid)row.Id,
                    idCurso: (Guid)row.IdCurso,
                    tipoExamen: (TipoExamen)row.TipoExamen,
                    temaExamen: (string)row.TemaExamen,
                    fechaLimiteDeEntrega: (DateTime)row.FechaLimiteDeEntrega,
                    fechaExamenCargado: (DateTime)row.FechaExamenCargado)
            )
            .ToList<Examen>();
        
        List<Inscripcion> inscripciones = inscripcionRows
            .GroupBy(r => (Guid)r.IdInscripcion)
            .Select(g =>
            {
                var firstRowInscripcion = g.First();
                var asistencias = g
                    .Where(r => r.IdAsistencia != null)
                    .Select(r => Asistencia.ReconstruirAsistencia(
                        id: (Guid)r.IdAsistencia,
                        idInscripcionEstudiante: (Guid)r.IdAsistenciaInscripcionEstudiante,
                        idClase: (Guid)r.IdAsistencaClase,
                        presente: (bool)r.AsistenciaPresente
                    ))
                    .ToList();
                    
                var entregasExamenes = g
                        .Where(r => r.IdEntrega != null)
                        .Select(r => EntregaDelExamen.ReconstruirEntrega(
                            id: (Guid)r.IdEntrega,
                            idExamen: (Guid)r.IdExamen,
                            estudianteIdInscripcion: (Guid)r.IdEntregaInscripcionEstudiante,
                            tipo: (TipoExamen)r.TipoEntregaExamen,
                            respuesta: (string)r.EntregaExamenRespuesta,
                            fechaEntregado: (DateTime)r.EntregaFechaEntrega,
                            fechaLimite: (DateTime)r.EntregaFechaLimiteExamen,
                            nota: r.EntregaNota != null ? new Nota((decimal)r.EntregaNota) : null,
                            comentarioDocente: r.EntregaComentarioDocente != null ? (string)r.EntregaComentarioDocente : null
                        ))
                    .ToList();

                return Inscripcion.ReconstruirInscripcion(
                    id:                   (Guid)firstRowInscripcion.IdInscripcion,
                    idEstudiante:         (Guid)firstRowInscripcion.IdInscripcionEstudiante,
                    idCurso:              (Guid)firstRowInscripcion.IdCurso,
                    fechaInscripcion:     (DateTime)firstRowInscripcion.FechaInscripcion,
                    activa:               (bool)firstRowInscripcion.Activa,
                    porcentajeAsistencia: (double)firstRowInscripcion.PorcentajeAsistencia,
                    entregas:             (List<EntregaDelExamen>)entregasExamenes,
                    asistencias:          (List<Asistencia>)asistencias
                );
            })
            .ToList();

        if (cursoRow is null)
            return null;
        
        DateRange duracion = DateRange.CrearDateRange(
            (DateTime)cursoRow.FechaInicio,
            (DateTime)cursoRow.FechaFin
        );
        
        
        Curso curso = Curso.ReconstruirCurso(
            id:            (Guid)cursoRow.CursoId,
            idProfesor:    (Guid)cursoRow.IdProfesor, 
            temario:       (string)cursoRow.Temario,
            nombre:        (string)cursoRow.Nombre,
            estado:        (EstadoCurso)cursoRow.Estado,
            duracion:      duracion,
            inscripciones: inscripciones,
            examenes:      examenes,
            clases:        clases
        );

        return curso;
    }

    public async Task<IEnumerable<CursoDTO>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Id, IdProfesor, Estado, Nombre, Tmeario, FechaInicio, FechaFin FROM Cursos";
        return await connection.QueryAsync<CursoDTO>(sql);
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
