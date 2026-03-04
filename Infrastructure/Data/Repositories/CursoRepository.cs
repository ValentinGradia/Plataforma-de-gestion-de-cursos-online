using System.Data;
using Dapper;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using static PlataformaDeGestionDeCursosOnline.Domain.Entities.Clase;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Data.Repositories;

public class CursoRepository : ICursoRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CursoRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

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
        // using var connection = _connectionFactory.CreateConnection();
        // var sql = @"
        // SELECT 
        //     c.Id, c.Nombre, c.Temario, c.Estado, c.Inicio, c.Fin,
        //     p.Id, p.Nombre, p.Email,
        //     cl.Id, cl.Estado,
        //     i.Id, i.IdEstudiante,
        //     ex.Id
        // FROM Cursos c
        // LEFT JOIN Profesores p      ON p.Id = c.ProfesorId
        // LEFT JOIN Clases cl         ON cl.CursoId = c.Id
        // LEFT JOIN Inscripciones i   ON i.CursoId = c.Id
        // LEFT JOIN Examenes ex       ON ex.CursoId = c.Id
        // WHERE c.Id = @Id";
        //
        // var row = await connection.QuerySingleOrDefaultAsync(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
        // if (row == null)
        //     return null;
        //
        // Curso curso = new Curso(
        //     Id: row.Id,
        //     profesor: row.Profesor,
        //     temario: row.Temario,
        //     nombre: row.Nombre,
        //     estado: 0, // Placeholder, no mapeo completo
        //     duracion: null!, // Placeholder, no mapeo completo
        //     inscripciones: new List<Inscripcion>(), // Placeholder, no mapeo completo
        //     examenes: new List<Examen>(), // Placeholder, no mapeo completo
        //     clases: new List<Clase>() // Placeholder, no mapeo completo
        // );)
        
        // return curso;
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
