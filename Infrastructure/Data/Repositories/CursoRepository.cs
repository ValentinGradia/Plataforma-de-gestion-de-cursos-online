using System.Data;
using Dapper;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases.ObjectValues;
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

    public async Task ActualizarAsync(Curso curso, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"UPDATE Cursos 
                    SET Nombre = @Nombre, 
                        Temario = @Temario,
                        Estado = @Estado,
                        FechaInicio = @FechaInicio,
                        FechaFin = @FechaFin
                    WHERE Id = @Id";

        await connection.ExecuteAsync(new CommandDefinition(
            sql,
            new
            {
                Id         = curso.Id,
                Nombre     = curso.Nombre,
                Temario    = curso.Temario,
                Estado     = curso.Estado,
                FechaInicio = curso.Duracion.Inicio,
                FechaFin    = curso.Duracion.Fin
            },
            cancellationToken: cancellationToken
        ));
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

        var clasesTasks = clasesRows.Select(r => this.ObtenerClasePorId((Guid)r.Id,cancellationToken));
        var clasesCargadas = await Task.WhenAll(clasesTasks);
        List<Clase> clases = clasesCargadas.Where(c => c is not null).Select(c => c!).ToList();
        
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

    Task<IEnumerable<Curso>> IRepository<Curso>.ObtenerTodosAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CursoDTO>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Id, IdProfesor, Estado, Nombre, Temario, FechaInicio, FechaFin FROM Cursos";
        return await connection.QueryAsync<CursoDTO>(sql);
    }

    public async Task<Curso> ObtenerCursoPorIdModeloExamen(Guid IdModeloExamen, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT c.Id FROM Cursos c JOIN ModelosExamen me ON me.IdCurso = c.Id WHERE me.Id = @IdModeloExamen";
        var IdCurso =  await connection.QuerySingleOrDefaultAsync<Guid>(sql, new { IdModeloExamen });
        
        if (IdCurso == Guid.Empty)
            throw new InvalidOperationException($"No se encontró un curso para el modelo de examen {IdModeloExamen}");
        
        //Reutilizamos el metodo ObtenerPorIdAsync para obtener el curso completo con toda su informacion,
        //dado que ya tenemos el Id del curso.
        return await ObtenerPorIdAsync(IdCurso, cancellationToken) ;
    }

    public async Task<Clase?> ObtenerClasePorId(Guid IdClase, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sqlClase = @"
        SELECT Id, Material, Estado, Fecha, IdCurso
        FROM Clases
        WHERE Id = @IdClase";
        
        var sqlAsistencias = @"
        SELECT Id, IdInscripcionEstudiante, IdClase, Presente
        FROM Asistencias
        WHERE IdClase = @IdClase";
        
        var sqlConsultas = "SELECT * from Consultas WHERE IdClase = @IdClase";
        
        var sql = sqlClase + ";" + sqlAsistencias + ";" + sqlConsultas;
        
        using var multi = await connection.QueryMultipleAsync(sql, new { IdClase });
        
        //Clase solo devuelve una, por eso usamos readFirstOrDefault
        var claseRow      =  await multi.ReadFirstOrDefaultAsync();
        var asistenciasRows    = (await multi.ReadAsync()).ToList();
        var consultasRows = (await multi.ReadAsync()).ToList();
        
        // Mapear asistencias
        var asistencias = asistenciasRows
            .Select(r => Asistencia.ReconstruirAsistencia(
                id:                     (Guid)r.Id,
                idInscripcionEstudiante:(Guid)r.IdInscripcionEstudiante,
                idClase:                (Guid)r.IdClase,
                presente:               (bool)r.Presente
            ))
            .ToList();
        
        var consultas = consultasRows
            .Select(r => Consulta.Reconstruir(
                id:            (Guid)r.Id,
                idClase:       (Guid)r.IdClase,
                idEstudiante:  (Guid)r.IdEstudiante,
                titulo:        (string)r.Titulo,
                descripcion:   (string)r.Descripcion,
                fechaConsulta: (DateTime)r.FechaConsulta
            ))
            .ToList();
        
        
        return Clase.ReconstruirClase(
            id:          (Guid)claseRow.Id,
            idCurso:     (Guid)claseRow.IdCurso,
            material:    (string)claseRow.Material,
            fecha:       (DateTime)claseRow.Fecha,
            estado:      (EstadoClase)claseRow.Estado,
            asistencias: asistencias,
            consultas: consultas
        );
        
    }

    public async Task<List<Estudiante>> ObtenerEstudiantesInscriptosEnCurso(Guid IdCurso, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"SELECT e.* FROM Estudiantes e
                    JOIN Inscripciones i ON i.IdEstudiante = e.Id
                    WHERE i.CursoId = @IdCurso";
        var estudiantes = await connection.QueryAsync<Estudiante>(sql, new { IdCurso });
        return estudiantes.ToList();
    }
}
