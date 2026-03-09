using System.Data;
using Dapper;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Data.Repositories;

public class EncuestasRepository : IEncuestasRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public EncuestasRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task GuardarAsync(Encuesta encuesta)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"INSERT INTO Encuestas (Id, IdCurso, IdEstudiante, CalificacionCurso, CalificacionMaterial, CalificacionDocente, Comentarios, FechaCreacion)
                   VALUES (@Id, @IdCurso, @IdEstudiante, @CalificacionCurso, @CalificacionMaterial, @CalificacionDocente, @Comentarios, @FechaCreacion)";
        await connection.ExecuteAsync(new CommandDefinition(sql, new
        {
            Id = encuesta.Id,
            IdCurso = encuesta.IdCurso,
            IdEstudiante = encuesta.IdEstudiante,
            CalificacionCurso = encuesta.CalificacionCurso.Valor,
            CalificacionMaterial = encuesta.CalificacionMaterial.Valor,
            CalificacionDocente = encuesta.CalificacionDocente.Valor,
            Comentarios = encuesta.Comentarios,
            FechaCreacion = encuesta.FechaCreacion
        }));
    }

    public async Task ActualizarAsync(Encuesta encuesta, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"UPDATE Encuestas SET CalificacionCurso = @CalificacionCurso, 
                     CalificacionCurso = @CalificacionCurso,
                     CalificacionMaterial = @CalificacionMaterial, 
                     CalificacionDocente = @CalificacionDocente, 
                     Comentarios = @Comentarios 
                    WHERE Id = @Id";
        await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = encuesta.Id, CalificacionCurso = encuesta.CalificacionCurso.Valor, 
            CalificacionMaterial = encuesta.CalificacionMaterial.Valor, CalificacionDocente = encuesta.CalificacionDocente.Valor,
            Comentarios = encuesta.Comentarios
        }, cancellationToken: cancellationToken));
    }

    public async Task<Encuesta?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"SELECT Id, IdCurso, IdEstudiante, CalificacionCurso, CalificacionMaterial, 
                           CalificacionDocente, Comentarios, FechaCreacion 
                    FROM Encuestas WHERE Id = @Id";

        var row = await connection.QuerySingleOrDefaultAsync(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));

        if (row is null) return null;

        return Encuesta.Reconstruir(
            (Guid)row.Id,
            (Guid)row.IdCurso,
            (Guid?)row.IdEstudiante,
            (int)row.CalificacionCurso,
            (int)row.CalificacionMaterial,
            (int)row.CalificacionDocente,
            (string)row.Comentarios,
            (DateTime)row.FechaCreacion
        );
    }

    public async Task<IEnumerable<Encuesta>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"SELECT Id, IdCurso, IdEstudiante, CalificacionCurso, CalificacionMaterial, 
                           CalificacionDocente, Comentarios, FechaCreacion 
                    FROM Encuestas";

        var rows = await connection.QueryAsync(sql);

        return rows.Select(row => Encuesta.Reconstruir(
            (Guid)row.Id,
            (Guid)row.IdCurso,
            (Guid?)row.IdEstudiante,
            (int)row.CalificacionCurso,
            (int)row.CalificacionMaterial,
            (int)row.CalificacionDocente,
            (string)row.Comentarios,
            (DateTime)row.FechaCreacion
        ));
    }
}
