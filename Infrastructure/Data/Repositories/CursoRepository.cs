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

        // query para el curso + profesor
        var sqlCurso = @"
            SELECT 
                c.Id AS CursoId,
                c.Nombre,
                c.Temario,
                c.Estado,
                c.FechaInicio,
                c.FechaFin,
                c.IdProfesor AS IdProfesor
            FROM Cursos c 
            LEFT JOIN Profesores p ON p.Id = c.IdProfesor 
            WHERE c.Id = @Id";

        // query para clases + asistencias + consultas (evita N+1 por cada clase)
        var sqlClases = @"
            SELECT 
                c.Id                AS ClaseId,
                c.IdCurso           AS ClaseIdCurso,
                c.Material          AS ClaseMaterial,
                c.Estado            AS ClaseEstado,
                c.Fecha             AS ClaseFecha,

                a.Id                        AS IdAsistencia,
                a.IdInscripcionEstudiante   AS IdAsistenciaInscripcionEstudiante,
                a.IdClase                   AS IdAsistencaClase,
                a.Presente                  AS AsistenciaPresente,

                co.Id               AS ConsultaId,
                co.IdClase          AS ConsultaIdClase,
                co.IdEstudiante     AS ConsultaIdEstudiante,
                co.Titulo           AS ConsultaTitulo,
                co.Descripcion      AS ConsultaDescripcion,
                co.FechaConsulta    AS ConsultaFechaConsulta
            FROM Clases c
            LEFT JOIN Asistencias a ON a.IdClase = c.Id
            LEFT JOIN Consultas co ON co.IdClase = c.Id
            WHERE c.IdCurso = @Id";

        // query para el curso + examenes
        var sqlExamenes = @"
            SELECT * FROM Examenes WHERE IdCurso = @Id";

        // query para el curso + inscripciones + asistencias + entregas del examen
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

        using var multi = await connection.QueryMultipleAsync(sql, new { Id = id });

        var cursoRow = await multi.ReadFirstOrDefaultAsync();
        var clasesRows = (await multi.ReadAsync()).ToList();
        var examenesRows = (await multi.ReadAsync()).ToList();
        var inscripcionRows = (await multi.ReadAsync()).ToList();

        List<Clase> clases = clasesRows
            .Where(r => r.ClaseId != null)
            .GroupBy(r => (Guid)r.ClaseId)
            .Select(g =>
            {
                var first = g.First();

                var asistencias = g
                    .Where(r => r.IdAsistencia != null)
                    .Select(r => Asistencia.ReconstruirAsistencia(
                        id: (Guid)r.IdAsistencia,
                        idInscripcionEstudiante: (Guid)r.IdAsistenciaInscripcionEstudiante,
                        idClase: (Guid)r.IdAsistencaClase,
                        presente: (bool)r.AsistenciaPresente
                    ))
                    .ToList();

                var consultas = g
                    .Where(r => r.ConsultaId != null)
                    .Select(r => Consulta.Reconstruir(
                        id: (Guid)r.ConsultaId,
                        idClase: (Guid)r.ConsultaIdClase,
                        idEstudiante: (Guid)r.ConsultaIdEstudiante,
                        titulo: (string)r.ConsultaTitulo,
                        descripcion: (string)r.ConsultaDescripcion,
                        fechaConsulta: (DateTime)r.ConsultaFechaConsulta
                    ))
                    .ToList();

                return Clase.ReconstruirClase(
                    id: (Guid)first.ClaseId,
                    idCurso: (Guid)first.ClaseIdCurso,
                    material: (string)first.ClaseMaterial,
                    fecha: (DateTime)first.ClaseFecha,
                    estado: (EstadoClase)first.ClaseEstado,
                    asistencias: asistencias,
                    consultas: consultas
                );
            })
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
                    id: (Guid)firstRowInscripcion.IdInscripcion,
                    idEstudiante: (Guid)firstRowInscripcion.IdInscripcionEstudiante,
                    idCurso: (Guid)firstRowInscripcion.IdCurso,
                    fechaInscripcion: (DateTime)firstRowInscripcion.FechaInscripcion,
                    activa: (bool)firstRowInscripcion.Activa,
                    porcentajeAsistencia: (double)firstRowInscripcion.PorcentajeAsistencia,
                    entregas: (List<EntregaDelExamen>)entregasExamenes,
                    asistencias: (List<Asistencia>)asistencias
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
            id: (Guid)cursoRow.CursoId,
            idProfesor: (Guid)cursoRow.IdProfesor,
            temario: (string)cursoRow.Temario,
            nombre: (string)cursoRow.Nombre,
            estado: (EstadoCurso)cursoRow.Estado,
            duracion: duracion,
            inscripciones: inscripciones,
            examenes: examenes,
            clases: clases
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

    public async Task InsertarClaseAsync(Clase clase, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"INSERT INTO Clases (Id, IdCurso, Material, Estado, Fecha)
                             VALUES (@Id, @IdCurso, @Material, @Estado, @Fecha)";

        await connection.ExecuteAsync(new CommandDefinition(
            sql,
            new
            {
                Id = clase.Id,
                IdCurso = clase.IdCurso,
                Material = clase.Material,
                Estado = clase.Estado,
                Fecha = clase.Fecha
            },
            cancellationToken: cancellationToken));
    }

    public async Task ActualizarClaseAsync(Clase clase, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"UPDATE Clases
                             SET Material = @Material,
                                 Estado = @Estado,
                                 Fecha = @Fecha
                             WHERE Id = @Id";

        await connection.ExecuteAsync(new CommandDefinition(
            sql,
            new
            {
                Id = clase.Id,
                Material = clase.Material,
                Estado = clase.Estado,
                Fecha = clase.Fecha
            },
            cancellationToken: cancellationToken));
    }

    public async Task<Examen?> ObtenerExamenPorIdAsync(Guid idExamen, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"SELECT Id, IdCurso, TipoExamen, TemaExamen, FechaLimiteDeEntrega, FechaExamenCargado
                             FROM Examenes
                             WHERE Id = @IdExamen";

        var row = await connection.QueryFirstOrDefaultAsync(new CommandDefinition(
            sql,
            new { IdExamen = idExamen },
            cancellationToken: cancellationToken));

        if (row is null)
            return null;

        return Examen.ReconstruirExamen(
            id: (Guid)row.Id,
            idCurso: (Guid)row.IdCurso,
            tipoExamen: (TipoExamen)row.TipoExamen,
            temaExamen: (string)row.TemaExamen,
            fechaLimiteDeEntrega: (DateTime)row.FechaLimiteDeEntrega,
            fechaExamenCargado: (DateTime)row.FechaExamenCargado);
    }

    public async Task InsertarExamenAsync(Examen examen, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"INSERT INTO Examenes (Id, IdCurso, TipoExamen, TemaExamen, FechaLimiteDeEntrega, FechaExamenCargado)
                             VALUES (@Id, @IdCurso, @TipoExamen, @TemaExamen, @FechaLimiteDeEntrega, @FechaExamenCargado)";

        await connection.ExecuteAsync(new CommandDefinition(
            sql,
            new
            {
                Id = examen.Id,
                IdCurso = examen.IdCurso,
                TipoExamen = examen.Tipo,
                TemaExamen = examen.TemaExamen,
                FechaLimiteDeEntrega = examen.FechaLimiteDeEntrega,
                FechaExamenCargado = examen.FechaExamenCargado
            },
            cancellationToken: cancellationToken));
    }

    public async Task ActualizarExamenAsync(Examen examen, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"UPDATE Examenes
                             SET TipoExamen = @TipoExamen,
                                 TemaExamen = @TemaExamen,
                                 FechaLimiteDeEntrega = @FechaLimiteDeEntrega
                             WHERE Id = @Id";

        await connection.ExecuteAsync(new CommandDefinition(
            sql,
            new
            {
                Id = examen.Id,
                TipoExamen = examen.Tipo,
                TemaExamen = examen.TemaExamen,
                FechaLimiteDeEntrega = examen.FechaLimiteDeEntrega
            },
            cancellationToken: cancellationToken));
    }

    public async Task<EntregaDelExamen?> ObtenerEntregaExamenPorIdAsync(Guid idEntrega, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"SELECT Id, IdExamen, IdInscripcionEstudiante, TipoExamen, Respuesta, FechaEntrega, FechaLimiteExamen, ValorNota, ComentarioDocente
                             FROM EntregasDelExamen
                             WHERE Id = @IdEntrega";

        var row = await connection.QueryFirstOrDefaultAsync(new CommandDefinition(
            sql,
            new { IdEntrega = idEntrega },
            cancellationToken: cancellationToken));

        if (row is null)
            return null;

        return EntregaDelExamen.ReconstruirEntrega(
            id: (Guid)row.Id,
            idExamen: (Guid)row.IdExamen,
            estudianteIdInscripcion: (Guid)row.IdInscripcionEstudiante,
            tipo: (TipoExamen)row.TipoExamen,
            respuesta: (string)row.Respuesta,
            fechaEntregado: (DateTime)row.FechaEntrega,
            fechaLimite: (DateTime)row.FechaLimiteExamen,
            nota: row.ValorNota != null ? new Nota((decimal)row.ValorNota) : null,
            comentarioDocente: row.ComentarioDocente != null ? (string)row.ComentarioDocente : null);
    }

    public async Task InsertarEntregaExamenAsync(EntregaDelExamen entrega, Guid idExamen, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"INSERT INTO EntregasDelExamen (Id, IdExamen, IdInscripcionEstudiante, TipoExamen, Respuesta, FechaEntrega, FechaLimiteExamen, ValorNota, ComentarioDocente)
                             VALUES (@Id, @IdExamen, @IdInscripcionEstudiante, @TipoExamen, @Respuesta, @FechaEntrega, @FechaLimiteExamen, @ValorNota, @ComentarioDocente)";

        await connection.ExecuteAsync(new CommandDefinition(
            sql,
            new
            {
                Id = entrega.Id,
                IdExamen = idExamen,
                IdInscripcionEstudiante = entrega.IdInscripcionEstudiante,
                TipoExamen = entrega.Tipo,
                Respuesta = entrega.Respuesta,
                FechaEntrega = entrega.FechaEntregado,
                FechaLimiteExamen = entrega.FechaLimiteExamen,
                ValorNota = entrega.Nota != null ? entrega.Nota.Valor : (decimal?)null,
                ComentarioDocente = entrega.ComentarioDocente
            },
            cancellationToken: cancellationToken));
    }

    public async Task ActualizarEntregaExamenAsync(EntregaDelExamen entrega, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"UPDATE EntregasDelExamen
                             SET TipoExamen = @TipoExamen,
                                 Respuesta = @Respuesta,
                                 FechaEntrega = @FechaEntrega,
                                 FechaLimiteExamen = @FechaLimiteExamen,
                                 ValorNota = @ValorNota,
                                 ComentarioDocente = @ComentarioDocente
                             WHERE Id = @Id";

        await connection.ExecuteAsync(new CommandDefinition(
            sql,
            new
            {
                Id = entrega.Id,
                TipoExamen = entrega.Tipo,
                Respuesta = entrega.Respuesta,
                FechaEntrega = entrega.FechaEntregado,
                FechaLimiteExamen = entrega.FechaLimiteExamen,
                ValorNota = entrega.Nota != null ? entrega.Nota.Valor : (decimal?)null,
                ComentarioDocente = entrega.ComentarioDocente
            },
            cancellationToken: cancellationToken));
    }

    public async Task<Consulta?> ObtenerConsultaPorIdAsync(Guid idConsulta, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"SELECT Id, IdClase, IdEstudiante, Titulo, Descripcion, FechaConsulta
                             FROM Consultas
                             WHERE Id = @IdConsulta";

        var row = await connection.QueryFirstOrDefaultAsync(new CommandDefinition(
            sql,
            new { IdConsulta = idConsulta },
            cancellationToken: cancellationToken));

        if (row is null)
            return null;

        return Consulta.Reconstruir(
            id: (Guid)row.Id,
            idClase: (Guid)row.IdClase,
            idEstudiante: (Guid)row.IdEstudiante,
            titulo: (string)row.Titulo,
            descripcion: (string)row.Descripcion,
            fechaConsulta: (DateTime)row.FechaConsulta);
    }

    public async Task InsertarConsultaAsync(Consulta consulta, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"INSERT INTO Consultas (Id, IdClase, IdEstudiante, Titulo, Descripcion, FechaConsulta)
                             VALUES (@Id, @IdClase, @IdEstudiante, @Titulo, @Descripcion, @FechaConsulta)";

        await connection.ExecuteAsync(new CommandDefinition(
            sql,
            new
            {
                Id = consulta.Id,
                IdClase = consulta.IdClase,
                IdEstudiante = consulta.IdEstudiante,
                Titulo = consulta.Titulo,
                Descripcion = consulta.Descripcion,
                FechaConsulta = consulta.FechaConsulta
            },
            cancellationToken: cancellationToken));
    }

    public async Task ActualizarConsultaAsync(Consulta consulta, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        const string sql = @"UPDATE Consultas
                             SET Titulo = @Titulo,
                                 Descripcion = @Descripcion,
                                 FechaConsulta = @FechaConsulta
                             WHERE Id = @Id";

        await connection.ExecuteAsync(new CommandDefinition(
            sql,
            new
            {
                Id = consulta.Id,
                Titulo = consulta.Titulo,
                Descripcion = consulta.Descripcion,
                FechaConsulta = consulta.FechaConsulta
            },
            cancellationToken: cancellationToken));
    }

    public async Task<List<Estudiante>> ObtenerEstudiantesInscriptosEnCurso(Guid IdCurso, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"SELECT e.* FROM Estudiantes e
                    JOIN Inscripciones i ON i.IdEstudiante = e.Id
                    WHERE i.IdCurso = @IdCurso";
        var estudiantes = await connection.QueryAsync<Estudiante>(sql, new { IdCurso });
        return estudiantes.ToList();
    }
}
