using System.Data;
using Dapper;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private IUsuarioRepository _usuarioRepositoryImplementation;

    public UsuarioRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task GuardarAsync(Usuario objeto)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "INSERT INTO Usuarios (Id, Nombre, Apellido, Email) VALUES (@Id, @Nombre, @Apellido, @Email)";
        await connection.ExecuteAsync(sql, new { Id = objeto.Id, Nombre = objeto.Nombre, Apellido = objeto.Apellido, Email = objeto.Email });
    }

    public async Task ActualizarAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido WHERE Id = @Id";
        await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = usuario.Id, Nombre = usuario.Nombre, Apellido = usuario.Apellido }, cancellationToken: cancellationToken));
    }

    public async Task<Usuario?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Id, Nombre, Apellido, Email FROM Usuarios WHERE Id = @Id";
        var row = await connection.QuerySingleOrDefaultAsync(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
        // No reconstructo el objeto Usuario (abstracto) aquí; devolver null como placeholder.
        return null;
    }

    public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Id, Nombre, Apellido, Email FROM Usuarios";
        var rows = await connection.QueryAsync(sql);
        return Enumerable.Empty<Usuario>();
    }
}
