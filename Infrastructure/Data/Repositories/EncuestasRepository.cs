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

    public async Task GuardarAsync(Encuesta objeto)
    {
        // using var connection = _connectionFactory.CreateConnection();
        // var sql = "INSERT INTO Encuestas (Id, Titulo, IdCurso) VALUES (@Id, @Titulo, @IdCurso)";
        // await connection.ExecuteAsync(sql, new { Id = objeto.Id, Titulo = objeto.Titulo, IdCurso = objeto.IdCurso });
    }

    public async Task ActualizarAsync(Encuesta clase, CancellationToken cancellationToken)
    {
        // using var connection = _connectionFactory.CreateConnection();
        // var sql = "UPDATE Encuestas SET Titulo = @Titulo WHERE Id = @Id";
        // await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = clase.Id, Titulo = clase.Titulo }, cancellationToken: cancellationToken));
    }

    public async Task<Encuesta?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Id, Titulo, IdCurso FROM Encuestas WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<Encuesta>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
    }

    public async Task<IEnumerable<Encuesta>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Id, Titulo, IdCurso FROM Encuestas";
        return await connection.QueryAsync<Encuesta>(sql);
    }
}
